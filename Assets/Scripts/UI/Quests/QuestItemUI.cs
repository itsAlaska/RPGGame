using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using TMPro;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI progress;

        private Quest currentQuest;
        
        public void Setup(Quest quest)
        {
            currentQuest = quest;
            title.text = quest.GetTitle();
            progress.text = $"0/{quest.GetObjectiveCount()}";
        }

        public Quest GetQuest()
        {
            return currentQuest;
        }
    }
}

