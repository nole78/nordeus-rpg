using Assets.Scripts.Database;
using Assets.Scripts.UI;
using NordeusRPG.DTOs;
using NordeusRPG.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthBarSlider heroHealthbar;
    public HealthBarSlider enemyHealthbar;
    public MoveIconDatabase moveDb;
    public List<MoveUI> moves;
    void Start() 
    {
        var hero = GameManager.Instance.Hero;
        var enemy = GameManager.Instance.CurrentEnemy;

        BattleManager.Instance.Init(hero, enemy);

        heroHealthbar.SetMaxHealth(hero.Health.MaxHealth);
        heroHealthbar.SetHealth(hero.Health.CurrentHealth);
        enemyHealthbar.SetMaxHealth(enemy.Health.MaxHealth);
        enemyHealthbar.SetHealth(enemy.Health.CurrentHealth);

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

    public void Refresh(BattleState state)
    {
        heroHealthbar.SetHealth(state.Hero.Health.CurrentHealth);
        enemyHealthbar.SetHealth(state.Enemy.Health.CurrentHealth);

        if (state.Hero.Health.CurrentHealth <= 0 || state.Enemy.Health.CurrentHealth <= 0)
        {
            if(!GameManager.Instance.IsEnemyDefeated(state.Enemy.Id))
                GameManager.Instance.MarkEnemyDefeated(state.Enemy.Id);
            SceneManager.LoadScene("Map");
        }
    }
}
