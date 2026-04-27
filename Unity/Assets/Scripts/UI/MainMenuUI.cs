using NordeusRPG.DTOs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public ApiClient api;

        public void OnStartGameClicked()
        {
            Debug.Log("Klik!");

            StartCoroutine(api.GetRunConfig(OnSucces, OnError));
        }
        private void OnSucces(RunConfigResponse config)
        {
            GameManager.Instance.CurrentConfig = config;

            SceneManager.LoadScene("Map");
        }
        private void OnError(string error)
        {
            Debug.LogError(error);
        }

    }
}
