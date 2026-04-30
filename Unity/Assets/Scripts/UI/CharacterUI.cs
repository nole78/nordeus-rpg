using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CharacterUI : MonoBehaviour
    {
        public HealthBarSlider healthbar;
        public TextMeshProUGUI characterName;
        public Image image;
        private CharacterVisual characterVisual;

        public void Init(CharacterVisual visual,int maxHealth,string name)
        {
            characterVisual = visual;
            healthbar.SetMaxHealth(maxHealth);
            characterName.text = name;

            image.sprite = visual.Idle;
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
            // TODO: add heal sprite and implement logic here
        }
        public void ApplyEffect()
        {
            // TODO: add effect icons to character prefab and implement effect show logic here
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
