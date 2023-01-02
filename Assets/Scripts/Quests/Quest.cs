using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private List<string> objectives = new List<string>();

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }

        public bool HasObjective(string objective)
        {
            return objectives.Contains(objective);
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }
    }
}

