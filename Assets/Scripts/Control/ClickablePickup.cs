using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using RPG.Inventories;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;

        void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandlePickup(callingController);
            }

            return true;
        }

        private void HandlePickup(PlayerController callingController)
        {
            if (callingController.GetComponent<Inventory>().HasSpaceFor(pickup.GetItem()))
            {
                HandleQuestObjective(pickup.GetItem(), callingController);

                pickup.PickupItem();
            }
        }

        public void HandleQuestObjective(InventoryItem item, PlayerController callingController)
        {
            if (item.GetIsQuestItem())
            {
                QuestGiver questGiver = GameObject.FindGameObjectWithTag("Quest Giver").GetComponent<QuestGiver>();
                GameObject pickupSpawner = GameObject.FindGameObjectWithTag("Quest Pickup");
                if (pickupSpawner.name == $"{item.name} Pickup Spawner")
                {
                    pickupSpawner.GetComponent<QuestCompletion>().CompleteObjective();
        
                    // Inventory inventory = callingController.GetComponent<Inventory>();
                    // int itemSlot = inventory.FindSlot(item);
                    // int amt = inventory.GetNumberInSlot(itemSlot);
                    // inventory.RemoveFromSlot(itemSlot, amt);
                }
            }
        }
    }
}