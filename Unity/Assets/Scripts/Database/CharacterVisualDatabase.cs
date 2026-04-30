using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Database
{
    [CreateAssetMenu(menuName = "Game/Character Visuals")]
    public class CharacterVisualDatabase : ScriptableObject
    {
        public List<CharacterVisual> characters;
        private Dictionary<string, CharacterVisual> _map;


        public void Init()
        {
            _map = characters.ToDictionary(c => c.Id, c => c);
        }

        public CharacterVisual GetSprite(string id)
        {
            return _map.TryGetValue(id, out var visual) ? visual : null;
        }
    }
}
