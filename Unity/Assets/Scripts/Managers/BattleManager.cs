using Newtonsoft.Json;
using NordeusRPG.DTOs;
using NordeusRPG.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    private BattleState _state;

    private void Awake()
    {
        Instance = this;
    }

    public void Init(Character hero,Character enemy)
    {
        _state = new BattleState
        {
            Hero = hero,
            Enemy = enemy,
            CurrentTurn = 1
        };
    }

    public void PlayMove(string moveId)
    {
        var request = new NextMoveRequest
        {
            PlayerMove = moveId,
            CurrentState = _state
        };

        StartCoroutine(ApiClient.Instance.SendNextMove(request, OnSucces, OnError));
    }

    private void OnSucces(NextMoveResponse response)
    {
        _state = response.UpdatedState;
        Debug.Log(JsonConvert.SerializeObject(response));
        Debug.Log(_state.Enemy.Health.CurrentHealth);
        FindObjectOfType<BattleUI>().Refresh(_state);
    }

    private void OnError(string err)
    {
        Debug.LogError(err);
    }
}
