using Assets.Scripts.Database;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using NordeusRPG.Enums;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    // Characters
    public CharacterUI heroUI;
    public CharacterUI enemyUI;
    public SummaryUI summary;
    public MoveIconDatabase moveDb;
    public CharacterVisualDatabase characterDb;
    public EffectIconDatabase effectDb;
    public List<MoveUI> moves;
    public BattleLogUI battleLog;
    void Start() 
    {
        var hero = GameManager.Instance.Player.Hero;
        var enemy = GameManager.Instance.CurrentEnemy;
        BattleManager.Instance.Init(hero, enemy);
        characterDb.Init();
        effectDb.Init();
        heroUI.Init(characterDb.GetSprite(hero.Id),hero.Health.MaxHealth,hero.Name,effectDb);
        enemyUI.Init(characterDb.GetSprite(enemy.Id), enemy.Health.MaxHealth, enemy.Name,effectDb);
        SetupMoves(hero);
    }

    private void SetupMoves(Character hero)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            var moveUI = moves[i];
            var moveData = hero.Moves[i];
            var moveSprite = moveDb.GetSprite(moveData.Id);

            moveUI.Init(() =>
            {
                BattleManager.Instance.PlayMove(moveData.Id);
                DisableButtons();
            }, moveData.Name,moveSprite);
        }
    }
    public void Refresh(BattleState state,List<CombatEvent> events)
    {
        StartCoroutine(PlaySequence(events, state));
    }
    IEnumerator PlaySequence(List<CombatEvent> events, BattleState state)
    {
        var tempHeroHp = state.Hero.Health.CurrentHealth;
        var tempEnemyHp = state.Enemy.Health.CurrentHealth;

        // rewind health through events
        foreach (var e in events)
        {
            if (e.Kind == MoveKind.Damage)
            {
                if (e.TargetId == state.Hero.Id)
                    tempHeroHp += e.Value;
                else
                    tempEnemyHp += e.Value;
            }

            if (e.Kind == MoveKind.Heal)
            {
                if (e.TargetId == state.Hero.Id)
                    tempHeroHp -= e.Value;
                else
                    tempEnemyHp -= e.Value;
            }
        }

        var heroTempEffects = state.Hero.StatusEffects;
        var enemyTempEffect = state.Enemy.StatusEffects;
        // rewind effects through events 
        foreach (var e in events)
        {
            if (e.Kind == MoveKind.ApplyStatus)
            {
                if (e.TargetId == state.Hero.Id)
                    heroTempEffects.RemoveAll(ef => ef.Id == e.AppliedEffect.Id);
                else
                    enemyTempEffect.RemoveAll(ef => ef.Id == e.AppliedEffect.Id);
            }
        }

        var grouped = events.GroupBy(e => e.MoveId);
        foreach (var eventGroup in grouped)
        {
            LogAction(eventGroup.FirstOrDefault(), state);
            foreach (var e in eventGroup)
            {
                var attackerUI = e.AttackerId == state.Hero.Id ? heroUI : enemyUI;
                var targetUI = e.TargetId == state.Hero.Id ? heroUI : enemyUI;

                switch (e.Kind)
                {
                    case MoveKind.Damage:
                        {
                            attackerUI.Attack();
                            yield return new WaitForSeconds(0.2f);

                            if (e.TargetId == state.Hero.Id)
                            {
                                tempHeroHp -= e.Value;
                                heroUI.TakeDamage(tempHeroHp);
                            }
                            else
                            {
                                tempEnemyHp -= e.Value;
                                enemyUI.TakeDamage(tempEnemyHp);
                            }
                        }
                    ; break;
                    case MoveKind.Heal:
                        {
                            if (e.TargetId == state.Hero.Id)
                            {
                                tempHeroHp += e.Value;
                                heroUI.TakeDamage(tempHeroHp);
                            }
                            else
                            {
                                tempEnemyHp += e.Value;
                                enemyUI.TakeDamage(tempEnemyHp);
                            }
                        }
                        ; break;
                    case MoveKind.ApplyStatus:
                        {
                            if (e.TargetId == state.Hero.Id)
                            {
                                heroTempEffects.Add(e.AppliedEffect);
                                heroUI.DisplayEffects(heroTempEffects);
                            }
                            else
                            {
                                enemyTempEffect.Add(e.AppliedEffect);
                                enemyUI.DisplayEffects(enemyTempEffect);
                            }
                        }
                        ; break;
                }
                if (eventGroup.Last() == e)
                    yield return new WaitForSeconds(0.2f);
                else
                    yield return new WaitForSeconds(0.8f);
            }
            heroUI.DisplayEffects(state.Hero.StatusEffects);
            enemyUI.DisplayEffects(state.Enemy.StatusEffects);
        }

        if (state.Hero.Health.IsDead() || state.Enemy.Health.IsDead())
        {
            bool victory = false;
            if (state.Enemy.Health.IsDead())
            {
                if(!GameManager.Instance.IsEnemyDefeated(state.Enemy.Id))
                    GameManager.Instance.MarkEnemyDefeated(state.Enemy.Id);
                victory = true;
            }

            HandleBattleOver(victory);
            yield break;
        }
        else
        {
            EnableButtons();
        }
    }

    void LogAction(CombatEvent e, BattleState state)
    {
        bool isHero = e.AttackerId == state.Hero.Id;
        string attacker = isHero ? "Hero" : state.Enemy.Name;
        string moveName = isHero ? state.Hero.Moves.First(m => m.Id == e.MoveId).Name : state.Enemy.Moves.First(m => m.Id == e.MoveId).Name;
        battleLog.AddLog($"{attacker} used move {moveName}!",isHero);
    }
    void DisableButtons()
    {
        foreach (var m in moves)
            m.SetInteractable(false);
    }

    void EnableButtons()
    {
        foreach (var m in moves)
            m.SetInteractable(true);
    }

    void HandleBattleOver(bool victory)
    {
        Move learntMove = null;
        if(victory)
        {
            var enemyMoves = GameManager.Instance.CurrentEnemy.Moves;
            var playerMoves = GameManager.Instance.Player.Moves;
            var movePool = new List<Move>();
            foreach(var move in enemyMoves)
            {
                if (!playerMoves.Contains(move))
                    movePool.Add(move);
            }

            if(movePool.Count() != 0)
            {
                int idx = Random.Range(0, movePool.Count());
                learntMove = movePool[idx];
                GameManager.Instance.LearnMove(learntMove);
            }
           
        }
        summary.Show(victory,learntMove);
    }
}
