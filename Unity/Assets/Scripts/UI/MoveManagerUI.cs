using Assets.Scripts.Database;
using Assets.Scripts.Models;
using NordeusRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MoveManagerUI : MonoBehaviour
    {
        public GameObject panel;
        public Transform contentParent; // ScrollView Content
        public MoveUI movePrefab;
        public List<MoveUI> activeMoveSlots; // ona 4 dugmeta
        public MoveIconDatabase moveDb;
        public TextMeshProUGUI textTip;

        private Player _player;
        private MoveUI selectedActiveSlot = null;

        public void Init()
        {
            _player = GameManager.Instance.Player;
        }

        public void Open()
        {
            panel.SetActive(true);
            GenerateAllMoves();
            textTip.text = "Choose a move to change";
        }

        public void Close()
        {
            if(selectedActiveSlot != null)
                selectedActiveSlot.SetSelected(false);
            selectedActiveSlot = null;
            panel.SetActive(false);
        }

        void GenerateAllMoves()
        {
            foreach (Transform child in contentParent)
                Destroy(child.gameObject);
            Debug.Log(_player.Moves);
            foreach (var move in _player.Moves)
            {
                var instance = Instantiate(movePrefab, contentParent);

                bool isEquipped = _player.Hero.Moves.Exists(m => m.Id == move.Id);
                var sprite = moveDb.GetSprite(move.Id);

                instance.Init(() =>
                {
                    OnLearnedMoveClicked(move, instance);
                },
                move.Name,
                sprite
                );

                instance.SetInteractable(!isEquipped);
            }
        }

        public void OnActiveMoveClicked(MoveUI slot)
        {
            if (selectedActiveSlot != null)
            {
                if (selectedActiveSlot == slot)
                {
                    selectedActiveSlot.SetSelected(false);
                    textTip.text = "Choose a move to change";
                    selectedActiveSlot = null;
                    return;
                }
                selectedActiveSlot.SetSelected(false);
            }

            selectedActiveSlot = slot;
            selectedActiveSlot.SetSelected(true);
            textTip.text = "Choose a replacement move";
        }

        void OnLearnedMoveClicked(Move move, MoveUI learnedUI)
        {
            if (selectedActiveSlot == null)
                return;

            var heroMoves = _player.Hero.Moves;

            int index = activeMoveSlots.IndexOf(selectedActiveSlot);

            // zameni u modelu
            heroMoves[index] = move;
            var sprite = moveDb.GetSprite(move.Id);

            // refresh UI
            selectedActiveSlot.Init(
                () => OnActiveMoveClicked(selectedActiveSlot),
                move.Name,
                sprite
            );

            selectedActiveSlot.SetSelected(false);
            selectedActiveSlot = null;

            GenerateAllMoves(); // refresh lista (disable opet)
        }
    }
}
