using Assets.Scripts.Database;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using NordeusRPG.Enums;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    // Characters
    public CharacterUI heroUI;
    public CharacterUI enemyUI;

    public MoveIconDatabase moveDb;
    public CharacterVisualDatabase characterDb;
    public List<MoveUI> moves;
    void Start() 
    {
        var hero = GameManager.Instance.Hero;
        var enemy = GameManager.Instance.CurrentEnemy;
        BattleManager.Instance.Init(hero, enemy);
        characterDb.Init();
        heroUI.Init(characterDb.GetSprite(hero.Id),hero.Health.MaxHealth,hero.Name);
        enemyUI.Init(characterDb.GetSprite(enemy.Id), enemy.Health.MaxHealth, enemy.Name);
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
            }, moveData.Name,moveSprite);
        }
    }
    public void Refresh(BattleState state,List<CombatEvent> events)
    {
        StartCoroutine(PlaySequence(events, state));
        if (state.Hero.Health.IsDead() || state.Enemy.Health.IsDead())
        {
            if(state.Enemy.Health.IsDead() && !GameManager.Instance.IsEnemyDefeated(state.Enemy.Id))
                GameManager.Instance.MarkEnemyDefeated(state.Enemy.Id);
            SceneManager.LoadScene("Map");
        }
    }
    IEnumerator PlaySequence(List<CombatEvent> events, BattleState state)
    {
        foreach (var e in events)
        {
            var attackerUI = e.AttackerId == state.Hero.Id ? heroUI : enemyUI;
            var targetUI = e.TargetId == state.Hero.Id ? heroUI : enemyUI;

            if (e.Kind == MoveKind.Damage)
            {
                attackerUI.Attack();
                yield return new WaitForSeconds(0.2f);

                targetUI.TakeDamage(
                    e.TargetId == state.Hero.Id
                    ? state.Hero.Health.CurrentHealth
                    : state.Enemy.Health.CurrentHealth
                );
            }

            if (e.Kind == MoveKind.Heal)
            {
                targetUI.Heal(
                    e.TargetId == state.Hero.Id
                    ? state.Hero.Health.CurrentHealth
                    : state.Enemy.Health.CurrentHealth
                );
            }

            yield return new WaitForSeconds(0.4f);
        }
    }
}
