using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] private float maxMana = 15;

        private float mana;

        private void Awake()
        {
            mana = maxMana;
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