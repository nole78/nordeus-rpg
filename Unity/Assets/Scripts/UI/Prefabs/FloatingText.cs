using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Prefabs
{
    public class FloatingText : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public float duration = 1f;
        public float moveUpAmount = 50f;

        private Vector2 startPosition;
        private Coroutine currentRoutine;

        void Awake()
        {
            startPosition = text.rectTransform.anchoredPosition;
        }
        public void Show(int amount, bool isHeal)
        {
            // Reset animation if there is one already
            if (currentRoutine != null)
                StopCoroutine(currentRoutine);

            text.rectTransform.anchoredPosition = startPosition;

            text.text = (isHeal ? "+" : "-") + amount.ToString();
            text.color = isHeal ? Color.green : Color.red;

            currentRoutine = StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            float time = 0f;

            Vector3 startPos = text.rectTransform.anchoredPosition;
            Vector3 endPos = startPos + new Vector3(0, moveUpAmount, 0);

            Color startColor = text.color;
            startColor.a = 1f;
            text.color = startColor;

            while (time < duration)
            {
                float t = time / duration;

                // Move up
                text.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);

                // Fade out
                Color c = text.color;
                c.a = Mathf.Lerp(1f, 0f, t);
                text.color = c;

                time += Time.deltaTime;
                yield return null;
            }

            // Reset alpha to 0 on end
            Color finalColor = text.color;
            finalColor.a = 0f;
            text.color = finalColor;
        }
    }
}
