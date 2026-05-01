using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EffectUI : MonoBehaviour
    {
        public TextMeshProUGUI duration;
        public Image image;
        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }
        public void SetDuration(int duration)
        {
            this.duration.text = duration.ToString();
        }
    }
}
