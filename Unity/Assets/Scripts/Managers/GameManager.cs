using Assets.Scripts.Models;
using NordeusRPG.DTOs;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RunConfigResponse Config { get; private set; }
    public Player Player = new();
    public Character CurrentEnemy { get; private set; }
    public HashSet<string> DefeatedEnemies = new();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEnemy(Character enemy)
    {
        CurrentEnemy = enemy;
    }

    public void SetConfig(RunConfigResponse config)
    {
        Config = config;
        Player.Hero = config.Hero;
        Player.Moves = Player.Hero.Moves;
    }

    public void SetUsername(string username)
    {
        Player.Username = username;
    }

    public void LearnMove(Move move)
    {
        Player.Moves.Add(move);
    }

    public bool IsEnemyDefeated(string enemyId)
        => DefeatedEnemies.Contains(enemyId);

    public void MarkEnemyDefeated(string enemyId)
        => DefeatedEnemies.Add(enemyId);
}
