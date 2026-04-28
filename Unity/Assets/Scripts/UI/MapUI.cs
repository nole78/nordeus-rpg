using Assets.Scripts.UI;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUI : MonoBehaviour
{
    public List<MapNodeUI> nodes;
    public List<Character> enemies;
    void Start()
    {
        var config = GameManager.Instance.Config;
        enemies = config.Enemies;

        for(int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];

            nodes[i].Init(() =>
            {
                GameManager.Instance.SetEnemy(enemy);
                SceneManager.LoadScene("Battle");
            });

            bool unlocked = CanAccess(enemy);
            nodes[i].SetInteractable(unlocked);
        }
    }

    bool CanAccess(Character enemy)
    {
        int index = enemies.IndexOf(enemy);

        if (index == 0) return true;

        var prevEnemy = enemies[index - 1];

        return GameManager.Instance.IsEnemyDefeated(prevEnemy.Id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
