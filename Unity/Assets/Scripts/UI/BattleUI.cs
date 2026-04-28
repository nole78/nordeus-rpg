using Assets.Scripts.UI;
using NordeusRPG.DTOs;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Character _hero;
    private Character _enemy;
    public HealthBarSlider heroHealthbar;
    public HealthBarSlider enemyHealthbar;
    void Start() 
    {
        _hero = GameManager.Instance.Hero;
        _enemy = GameManager.Instance.CurrentEnemy;

        heroHealthbar.SetMaxHealth(_hero.Health.MaxHealth);
        heroHealthbar.SetHealth(_hero.Health.CurrentHealth);

        enemyHealthbar.SetMaxHealth(_enemy.Health.MaxHealth);
        enemyHealthbar.SetHealth(_enemy.Health.CurrentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
