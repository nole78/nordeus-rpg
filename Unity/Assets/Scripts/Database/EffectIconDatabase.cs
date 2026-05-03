using Assets.Scripts.Models;
using NordeusRPG.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Database
{
    [CreateAssetMenu(menuName = "Game/Effect Icons")]
    public class EffectIconDatabase : ScriptableObject
    {
        public List<EffectIconEntry> entries;

        private Dictionary<EffectType, Sprite> map;

        public void Init()
        {
            map = new Dictionary<EffectType, Sprite>();

            foreach (var e in entries)
                map[e.type] = e.sprite;
        }

        public Sprite GetSprite(EffectType type)
        {
            return map.TryGetValue(type, out var s) ? s : null;
        }
    }
}
