using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class CharacterVisual
    {
        public string Id;
        public Sprite Idle;
        public Sprite Attack;
        public Sprite Hit;
    }
}
