using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public class Pickup : MonoBehaviour
    {
        InventoryItem item;

        Inventory inventory;

        void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
        }

        /// <summary>
        /// Set the vital data after creating the prefab.
        /// </summary>
        /// <param name="item">The type of item this prefab represents.</param>
        public void Setup(InventoryItem item)
        {
            this.item = item;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}