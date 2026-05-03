using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class ExitScreenUI : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void onExitClicked()
        {
            Destroy(GameManager.Instance);
            SceneManager.LoadScene("MainMenu");
        }

        public void onExitAndSaveClicked()
        {
            SaveService.Save();
            Destroy(GameManager.Instance);
            SceneManager.LoadScene("MainMenu");
        }

        public void onCancelClicked()
        {
            gameObject.SetActive(false);
        }
    }
}
