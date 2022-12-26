using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.Inventories
{
    /// <summary>
    /// To be placed on anything that wishes to drop pickups into the world.
    /// Tracks the drops for saving and restoring.
    /// </summary>
    public class ItemDropper : MonoBehaviour, ISaveable
    {
        private List<Pickup> droppedItems = new List<Pickup>();

        /// <summary>
        /// Create a pickup at the current position.
        /// </summary>
        /// <param name="item">The item type for the pickup.</param>
        /// <param name="number">Amount of this item.</param>
        public void DropItem(InventoryItem item, int number)
        {
            // TODO
            SpawnPickup(item, number, GetDropLocation());
        }

        /// <summary>
        /// Override to set a custom method for locating a drop.
        /// </summary>
        /// <returns>The location the drop should be spawned.</returns>
        protected virtual Vector3 GetDropLocation()
        {
            return transform.position;
        }

        public void SpawnPickup(InventoryItem item, int number, Vector3 spawnLocation)
        {
            var pickup = item.SpawnPickup(spawnLocation, 1);
            droppedItems.Add(pickup);
        }

        [System.Serializable]
        struct DropRecord
        {
            public string itemID;
            public _mySerializableVector3 position;
        }

        object ISaveable.CaptureState()
        {
            RemoveDestroyedDrops();
            var droppedItemsList = new DropRecord[droppedItems.Count];
            for (int i = 0; i < droppedItemsList.Length; i++)
            {
                droppedItemsList[i].itemID = droppedItems[i].GetItem().GetItemID();
                droppedItemsList[i].position = new _mySerializableVector3(droppedItems[i].transform.position);
            }

            return droppedItemsList;
        }

        void ISaveable.RestoreState(object state)
        {
            var droppedItemsList = (DropRecord[])state;
            foreach (var item in droppedItemsList)
            {
                var pickupItem = InventoryItem.GetFromID(item.itemID);
                Vector3 position = item.position.ToVector();
                // TODO
                SpawnPickup(pickupItem, 1, position);
            }
        }

        void RemoveDestroyedDrops()
        {
            var newList = new List<Pickup>();
            foreach (var item in droppedItems)
            {
                if (item != null)
                {
                    newList.Add(item);
                }
            }

            droppedItems = newList;
        }
    }
}