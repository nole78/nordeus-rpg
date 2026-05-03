using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Database
{
    [CreateAssetMenu(menuName = "Game/Move Icons")]
    public class MoveIconDatabase : ScriptableObject
    {
        public List<MoveIconEntry> entries;
        private Dictionary<string, Sprite> _map;

        public Sprite GetSprite(string moveId)
        {
            if(_map == null)
            {
                _map = new Dictionary<string, Sprite>(entries.Count);
                foreach (var entry in entries)
                    _map[entry.moveId] = entry.sprite;
            }
            // TODO make placeholder sprite so that null is never returned
            return _map.TryGetValue(moveId, out var sprite) ? sprite : null;
        }
    }
}
