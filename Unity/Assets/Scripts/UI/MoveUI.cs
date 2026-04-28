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

        public void Init(Action onClick,string name)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick());
            text.text = name;
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }
    }
}
