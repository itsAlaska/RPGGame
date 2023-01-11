using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length == 0) return 0;

            if (levels.Length < level) return levels[^1];

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass cClass)
        {
            BuildLookup();

            float[] levels = lookupTable[cClass][stat];
            return levels.Length;
        }

        void BuildLookup()
        {
            if (lookupTable != null)
                return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass pClass in characterClasses)
            {
                var statLookUpTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat pStat in pClass.stats)
                {
                    statLookUpTable[pStat.stat] = pStat.levels;
                }

                lookupTable[pClass.characterClass] = statLookUpTable;
                float[] levels = new float[pClass.stats.Length];
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}