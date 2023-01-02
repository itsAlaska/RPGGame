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

        private QuestStatus currentStatus;
        
        public void Setup(QuestStatus status)
        {
            currentStatus = status;
            title.text = status.GetQuest().GetTitle();
            progress.text = $"{status.GetCompletedCount()}/{status.GetQuest().GetObjectiveCount()}";
        }

        public QuestStatus GetQuestStatus()
        {
            return currentStatus;
        }
    }
}

