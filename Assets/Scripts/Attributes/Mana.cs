using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] private float maxMana = 15;
        [SerializeField] private float manaRegenRate = 0;

        private float mana;

        private void Awake()
        {
            mana = maxMana;
        }

        private void Update()
        {
            if (mana < maxMana)
            {
                mana += manaRegenRate * Time.deltaTime;
                if (mana > maxMana)
                {
                    mana = maxMana;
                }
            }
        }

        public float GetMana()
        {
            return Mathf.Round(mana);
        }

        public float GetMaxMana()
        {
            return Mathf.Round(maxMana);
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana)
            {
                return false;
            }

            mana -= manaToUse;
            return true;
        }
    }
}