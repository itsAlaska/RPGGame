using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using TMPro;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Transform objectiveContainer;
        [SerializeField] private GameObject objectivePrefab;
        public void Setup(Quest quest)
        {
            title.text = quest.GetTitle();
            
            if (objectiveContainer.childCount > 0)
            {
                foreach (Transform child in objectiveContainer)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (string objective in quest.GetObjectives())
            {
                GameObject objectiveInstance = Instantiate(objectivePrefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective;
            }
            
        }
    }
}

