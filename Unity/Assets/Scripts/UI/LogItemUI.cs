using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LogItemUI : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void SetText(string message)
        {
            text.text = message;
        }

        public void SetAttackerColor(bool isHero)
        {
            text.color = isHero ? Color.green : Color.red;
        }
    }
}
