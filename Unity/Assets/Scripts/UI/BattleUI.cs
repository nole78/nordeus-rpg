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

        var grouped = events.GroupBy(e => e.ActionIndex);
        // TODO: add combat log using grouped and remove this logic with events
        for (int i = 0; i < events.Count; i++)
        {
            var attackerUI = events[i].AttackerId == state.Hero.Id ? heroUI : enemyUI;
            var targetUI = events[i].TargetId == state.Hero.Id ? heroUI : enemyUI;

            heroUI.DisplayEffects(state.Hero.StatusEffects);
            enemyUI.DisplayEffects(state.Enemy.StatusEffects);

            switch(events[i].Kind)
            {
                case MoveKind.Damage:
                {
                    attackerUI.Attack();
                    yield return new WaitForSeconds(0.2f);

                    if (events[i].TargetId == state.Hero.Id)
                    {
                        tempHeroHp -= events[i].Value;
                        heroUI.TakeDamage(tempHeroHp);
                    }
                    else
                    {
                        tempEnemyHp -= events[i].Value;
                        enemyUI.TakeDamage(tempEnemyHp);
                    }
                }
                ; break;
                case MoveKind.Heal:
                {
                    if (events[i].TargetId == state.Hero.Id)
                    {
                        tempHeroHp += events[i].Value;
                        heroUI.TakeDamage(tempHeroHp);
                    }
                    else
                    {
                        tempEnemyHp += events[i].Value;
                        enemyUI.TakeDamage(tempEnemyHp);
                    }
                } ; break;
                case MoveKind.ApplyStatus:
                {
                        if (events[i].TargetId == state.Hero.Id)
                        {
                            heroTempEffects.Add(events[i].AppliedEffect);
                            heroUI.DisplayEffects(heroTempEffects);
                        }
                        else
                        {
                            enemyTempEffect.Add(events[i].AppliedEffect);
                            enemyUI.DisplayEffects(enemyTempEffect);
                        }
                    }
                    ; break;
            }
            // Displays updated effect durations
            heroUI.DisplayEffects(state.Hero.StatusEffects);
            enemyUI.DisplayEffects(state.Enemy.StatusEffects);
            if (i != events.Count - 1)
                yield return new WaitForSeconds(0.8f);
            else
                yield return new WaitForSeconds(0.2f);
        }


        if (state.Hero.Health.IsDead() || state.Enemy.Health.IsDead())
        {
            bool victory = false;
            if (state.Enemy.Health.IsDead() && !GameManager.Instance.IsEnemyDefeated(state.Enemy.Id))
            {
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
            foreach(var move in enemyMoves)
            {
                if (playerMoves.Contains(move))
                    enemyMoves.Remove(move);
            }

            if(enemyMoves.Count() != 0)
            {
                int idx = Random.Range(0, enemyMoves.Count());
                learntMove = enemyMoves[idx];
                GameManager.Instance.LearnMove(learntMove);
            }
           
        }
        summary.Show(victory,learntMove);
    }
}
