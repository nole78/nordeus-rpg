using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBarSlider : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI healthText;

        public void SetMaxHealth(int max)
        {
            slider.maxValue = max;
            slider.value = max;
            SetText(max, max);
        }

        public void SetHealth(int current)
        {
            slider.value = current;
            SetText(current, (int)slider.maxValue);
        }

        public void SetNormalized(float percent)
        {
            slider.normalizedValue = percent;
            SetText((int)slider.value, (int)slider.maxValue);
        }

        public void SetText(int current, int max)
        {
            healthText.text = $"{current} / {max}";
        }

        public void SetHealthSmooth(int target)
        {
            StartCoroutine(Smooth(target));
            SetText(target, (int)slider.maxValue);
        }

        IEnumerator Smooth(int target)
        {
            float start = slider.value;
            float time = 0;

            while (time < 0.2f)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(start, target, time / 0.2f);
                yield return null;
            }

            slider.value = target;
        }
    }
}
