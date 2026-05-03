using Assets.Scripts.Database;
using Assets.Scripts.Models;
using Assets.Scripts.Services;
using Assets.Scripts.UI;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUI : MonoBehaviour
{
    public List<MapNodeUI> nodes;
    public List<MoveUI> selectedMoves;
    public MoveIconDatabase moveDb;
    public MoveManagerUI moveManager;
    public ExitScreenUI exitScene;

    private List<Character> _enemies;
    void Start()
    {
        var config = GameManager.Instance.Config;
        _enemies = config.Enemies;
        moveManager.Init();

        for(int i = 0; i < _enemies.Count; i++)
        {
            var enemy = _enemies[i];

            nodes[i].Init(() =>
            {
                GameManager.Instance.SetEnemy(enemy);
                SceneManager.LoadScene("Battle");
            });

            bool unlocked = CanAccess(enemy);
            nodes[i].SetInteractable(unlocked);
        }

        var moves = GameManager.Instance.Player.Hero.Moves;
        for (int i = 0; i < moves.Count && i < selectedMoves.Count; i++)
        {
            var move = moves[i];
            var moveSprite = moveDb.GetSprite(move.Id);
            var moveUI = selectedMoves[i];
            var slot = moveManager.activeMoveSlots[i];
            moveUI.Init(() =>
            {
                if(moveManager.isActiveAndEnabled)
                    moveManager.OnActiveMoveClicked(slot);
            },move.Name,moveSprite);
        }
    }

    bool CanAccess(Character enemy)
    {
        int index = _enemies.IndexOf(enemy);

        if (index == 0) return true;

        var prevEnemy = _enemies[index - 1];

        return GameManager.Instance.IsEnemyDefeated(prevEnemy.Id);
    }
}
