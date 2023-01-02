using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] private Quest[] tempQuests;
    [SerializeField] private QuestItemUI questPrefab;

    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Quest quest in tempQuests)
        {
            QuestItemUI questDisplay = Instantiate(questPrefab, transform);
            questDisplay.Setup(quest);
        }
    }
}