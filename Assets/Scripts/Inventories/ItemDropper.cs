using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Inventories
{
    /// <summary>
    /// To be placed on anything that wishes to drop pickups into the world.
    /// Tracks the drops for saving and restoring.
    /// </summary>
    public class ItemDropper : MonoBehaviour, ISaveable
    {
        private List<Pickup> droppedItems = new List<Pickup>();
        private List<DropRecord> otherSceneDroppedItems = new List<DropRecord>();

        /// <summary>
        /// Create a pickup at the current position.
        /// </summary>
        /// <param name="item">The item type for the pickup.</param>
        /// <param name="number">Amount of this item.</param>
        public void DropItem(InventoryItem item, int number)
        {
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
            var pickup = item.SpawnPickup(spawnLocation, number);
            droppedItems.Add(pickup);
        }

        [System.Serializable]
        struct DropRecord
        {
            public string itemID;
            public _mySerializableVector3 position;
            public int number;
            public int sceneBuildIndex;
        }

        object ISaveable.CaptureState()
        {
            RemoveDestroyedDrops();
            var droppedItemsList = new List<DropRecord>();
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            foreach (Pickup pickup in droppedItems)
            {
                var droppedItem = new DropRecord();
                droppedItem.itemID = pickup.GetItem().GetItemID();
                droppedItem.position = new _mySerializableVector3(pickup.transform.position);
                droppedItem.number = pickup.GetNumber();
                droppedItem.sceneBuildIndex = buildIndex;
                droppedItemsList.Add(droppedItem);
            }
            droppedItemsList.AddRange(otherSceneDroppedItems);
            return droppedItemsList;
        }

        void ISaveable.RestoreState(object state)
        {
            var droppedItemsList = (List<DropRecord>)state;
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            otherSceneDroppedItems.Clear();
            
            foreach (var item in droppedItemsList)
            {
                if (item.sceneBuildIndex != buildIndex)
                {
                    otherSceneDroppedItems.Add(item);
                    continue;
                }
                var pickupItem = InventoryItem.GetFromID(item.itemID);
                Vector3 position = item.position.ToVector();
                int number = item.number;
                SpawnPickup(pickupItem, number, position);
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