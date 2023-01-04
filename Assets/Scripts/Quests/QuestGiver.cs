using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private Quest quest;
        [SerializeField] private GameObject questPickup = null;

        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            
            questList.AddQuest(quest);
        }

        public void EnableQuestPickup()
        {
            questPickup.SetActive(true);
        }
    }
}

