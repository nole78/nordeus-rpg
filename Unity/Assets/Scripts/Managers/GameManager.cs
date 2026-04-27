using NordeusRPG.DTOs;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RunConfigResponse Config { get; private set; }
    public Character Hero => Config.Hero;
    public Character CurrentEnemy { get; private set; }
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
    }
}
