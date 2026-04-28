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

        public void Init(Action onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick());
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }
    }
}
