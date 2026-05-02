using Assets.Scripts.Database;
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
    public class MoveUI : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI text;
        public Image icon;
        public Image selectionHighlight;

        public void Init(Action onClick,string name,Sprite sprite)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick());
            text.text = name;

            icon.sprite = sprite;
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }
        public void SetSelected(bool value)
        {
            selectionHighlight.gameObject.SetActive(value);
        }
    }
}
