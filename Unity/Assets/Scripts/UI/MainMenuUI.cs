using NordeusRPG.DTOs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void OnStartGameClicked()
        {
            Debug.Log("Klik!");

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

    }
}
