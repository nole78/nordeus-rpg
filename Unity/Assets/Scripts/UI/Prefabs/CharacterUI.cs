using Assets.Scripts.Models;
using NordeusRPG.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Database;

namespace Assets.Scripts.UI
{
    public class CharacterUI : MonoBehaviour
    {
        public HealthBarSlider healthbar;
        public TextMeshProUGUI characterName;
        public Image image;
        public List<EffectUI> effects;
        private CharacterVisual characterVisual;
        private EffectIconDatabase _effectDb;

        public void Init(CharacterVisual visual,int maxHealth,string name,EffectIconDatabase effectDb)
        {
            characterVisual = visual;
            healthbar.SetMaxHealth(maxHealth);
            characterName.text = name;
            _effectDb = effectDb;
            image.sprite = visual.Idle;
            foreach(var effect in effects)
            {
                effect.SetVisibility(false);
            }
        }

        public void TakeDamage(int amount)
        {
            healthbar.SetHealthSmooth(amount);
            StartCoroutine(PlayHitAnimation());
        }

        public void Attack()
        {
            StartCoroutine(PlayAttackAnimation());
        }
        public void Heal(int amount)
        {
            healthbar.SetHealthSmooth(amount);
            // TODO: add heal animation and implement logic here
        }
        public void ApplyEffect()
        {
            // TODO: add effect apply animation and its logic here
        }
        public void DisplayEffects(List<StatusEffect> statusEffects)
        {
            foreach (var effect in effects)
            {
                effect.SetVisibility(false);
            }
            for (int i = 0; i < statusEffects.Count && i < effects.Count; i++)
            {
                var data = statusEffects[i];
                var ui = effects[i];

                ui.SetSprite(_effectDb.GetSprite(data.Type));
                ui.SetDuration(data.RemainingTurns);
                ui.SetVisibility(true);
            }
        }

        IEnumerator PlayAttackAnimation()
        {
            image.sprite = characterVisual.Attack;

            yield return new WaitForSeconds(0.3f);

            image.sprite = characterVisual.Idle;
        }

        IEnumerator PlayHitAnimation()
        {
            image.sprite = characterVisual.Hit;

            yield return new WaitForSeconds(0.2f);

            image.sprite = characterVisual.Idle;
        }
    }
}
