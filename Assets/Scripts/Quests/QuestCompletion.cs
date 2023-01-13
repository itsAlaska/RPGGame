using RPG.Attributes;
using RPG.Inventories;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] private Quest quest;
        [SerializeField] private string objective;
        [SerializeField] private InventoryItem questItem = null;

        public void CompleteObjective()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.CompleteObjective(quest, objective);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                RemoveQuestItem();
            }
        }

        public void RemoveQuestItem()
        {
            Inventory playerInventory = Inventory.GetPlayerInventory();



            int invSlot = playerInventory.FindSlotContainingItem(questItem);
            int slotAmt = playerInventory.GetNumberInSlot(invSlot);
            playerInventory.RemoveFromSlot(invSlot, slotAmt);
        }
    }
}