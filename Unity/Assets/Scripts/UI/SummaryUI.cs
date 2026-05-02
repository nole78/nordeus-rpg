using NordeusRPG.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SummaryUI : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public GameObject button;
        public RectTransform panel;

        public float speed = 800f;
        public float targetHeight = 2000f;

        private void Awake()
        {
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, 0);
            button.SetActive(false);
        }

        public void Show(bool victory,Move learntMove)
        {
            gameObject.SetActive(true);

            title.text = victory ? "VICTORY" : "DEFEAT";
            title.color = victory ? Color.green : Color.red;

            StopAllCoroutines();
            StartCoroutine(AnimateOpen());
            if (victory)
            {
                if(learntMove != null)
                    StartCoroutine(TypeText("You defeated the enemy!\n" +
                    "Learnt move: " + learntMove.Name + "!"));
                else
                    StartCoroutine(TypeText("You defeated the enemy!\n" +
                    "You already learned all enemy moves!"));
            }
            else
            {
                StartCoroutine(TypeText("You were defeated by the enemy!"));
            }
        }

        IEnumerator AnimateOpen()
        {
            float height = 0;

            while (height < targetHeight)
            {
                height += speed * Time.deltaTime;

                panel.sizeDelta = new Vector2(panel.sizeDelta.x, height);

                yield return null;
            }

            panel.sizeDelta = new Vector2(panel.sizeDelta.x, targetHeight);

            button.SetActive(true);

            // tek sad dugme
            button.SetActive(true);
        }

        IEnumerator TypeText(string fullText)
        {
            description.text = "";

            foreach (char c in fullText)
            {
                description.text += c;
                yield return new WaitForSeconds(0.02f);
            }
        }

        public void onContinueClicked()
        {
            SceneManager.LoadScene("Map");
        }

    }
}
