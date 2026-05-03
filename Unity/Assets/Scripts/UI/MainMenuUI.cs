using NordeusRPG.DTOs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button loadButton;
        void Awake()
        {
            loadButton.interactable = false;
            // TODO: chechk if there is saved run
        }
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

        public void OnExitClicked()
        {
            Application.Quit();
        }

        public void OnLoadGameCliecked()
        {
            // TODO: Add Load Game logic here
        }

    }
}
