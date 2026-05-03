using Assets.Scripts.Models;
using Assets.Scripts.Services;
using NordeusRPG.DTOs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button loadButton;
        private SaveData _data;
        void Awake()
        {
            _data = SaveService.Load();
            if (_data != null)
                loadButton.interactable = true;
            else
                loadButton.interactable = false;
        }
        public void OnStartGameClicked()
        {
            StartCoroutine(ApiClient.Instance.GetRunConfig(OnSucces, OnError));
        }
        private void OnSucces(RunConfigResponse config)
        {
            GameManager.Instance.SetConfig(config);

            SceneManager.LoadScene("Map");
        }
        private void OnError(string error)
        {
            Debug.LogError(error);
        }

        public void OnExitClicked()
        {
            Application.Quit();
        }

        public void OnLoadGameCliecked()
        {
            GameManager.Instance.SetConfig(_data.config);
            GameManager.Instance.Player.Moves = _data.player.moves;
            GameManager.Instance.Player.Hero.Moves = _data.player.selectedMoves;
            GameManager.Instance.Player.LevelSystem.Level = _data.player.level;
            GameManager.Instance.Player.LevelSystem.Experience= _data.player.experience;
            foreach (var enemy in _data.defeatedEnemiesId)
                GameManager.Instance.MarkEnemyDefeated(enemy);
            SceneManager.LoadScene("Map");
        }

    }
}
