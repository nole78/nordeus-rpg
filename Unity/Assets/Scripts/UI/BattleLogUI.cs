using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattleLogUI : MonoBehaviour
    {
        public Transform content;
        public LogItemUI logPrefab;
        public ScrollRect scrollRect;

        public void AddLog(string message, bool isHero)
        {
            var log = Instantiate(logPrefab, content);
            log.SetText(message);
            log.SetAttackerColor(isHero);

            // limit battle log to hold 50 messages max
            if (content.childCount > 50)
                Destroy(content.GetChild(0).gameObject);

            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}
