using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] private QuestItemUI questPrefab;
    
        void Start()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
    
            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate(questPrefab, transform);
                uiInstance.Setup(status);
            }
        }
    }
}
