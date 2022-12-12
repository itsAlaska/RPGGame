using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField]
        int startingLevel = 1;

        [SerializeField]
        CharacterClass characterClass;

        [SerializeField]
        Progression progression = null;

        [SerializeField]
        GameObject levelUpEffect;

        [SerializeField]
        GameObject selfHealEffect;

        int currentLevel = 0;

        public event Action onLevelUp;

        void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpEffect, transform.position, transform.rotation);
            Instantiate(selfHealEffect, transform.position, transform.rotation);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel())
                + GetAdditiveModifier(stat);
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                CalculateLevel();
            }
            return currentLevel;
        }

        int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null)
                return startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExpereinceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(
                    Stat.ExpereinceToLevelUp,
                    characterClass,
                    level
                );

                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

        float GetAdditiveModifier(Stat stat)
        {
            float total = 0;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
    }
}
