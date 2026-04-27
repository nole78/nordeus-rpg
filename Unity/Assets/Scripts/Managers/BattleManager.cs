using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Character hero;
    private Character enemy;
    void Start()
    {
        hero = GameManager.Instance.Hero;
        enemy = GameManager.Instance.CurrentEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
