using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MapNodeUI : MonoBehaviour
    {
        public Button button;
        public Image background;
        public void Init(Action onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick());
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }

        public void SetBeaten()
        {
            Color32 color = new Color32(93, 150, 91, 255);
            background.color = color;
        }
    }
}
