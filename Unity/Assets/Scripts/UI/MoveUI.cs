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
        public MoveIconDatabase iconDb;
        private string _moveId = string.Empty;

        public void Init(Action onClick,string name,string id)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick());
            text.text = name;
            _moveId = id;

            icon.sprite = iconDb.GetSprite(id);
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }

        public string GetId()
        {
            return _moveId;
        }
    }
}
