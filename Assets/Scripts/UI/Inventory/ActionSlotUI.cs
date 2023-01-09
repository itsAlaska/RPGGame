using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Abilities;
using RPG.Core.UI.Dragging;
using RPG.Inventories;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using InventoryItem = RPG.Inventories.InventoryItem;

namespace RPG.UI.Inventories
{
    /// <summary>
    /// The UI slot for the player action bar.
    /// </summary>
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] private InventoryItemIcon icon = null;
        [SerializeField] private int index = 0;
        [SerializeField] private Image cooldownOverlay = null;

        private ActionStore store;
        private CooldownStore cooldownStore;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            store = player.GetComponent<ActionStore>();
            cooldownStore = player.GetComponent<CooldownStore>();
            store.storeUpdated += UpdateIcon;
        }

        private void Update()
        {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(GetItem());
        }

        public void AddItems(InventoryItem item, int number)
        {
            store.AddAction(item, index, number);
        }

        public InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int GetNumber()
        {
            return store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            store.RemoveItems(index, number);
        }

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}

