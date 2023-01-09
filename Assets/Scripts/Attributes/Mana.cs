using GameDevTV.Utils;
using RPG.Stats;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        LazyValue<float> mana;

        private void Awake()
        {
            mana = new LazyValue<float>(GetMaxMana);
        }

        private void Update()
        {
            if (mana.value < GetMaxMana())
            {
                mana.value += GetRegenRate() * Time.deltaTime;
                if (mana.value > GetMaxMana())
                {
                    mana.value = GetMaxMana();
                }
            }
        }

        public float GetMana()
        {
            return Mathf.Round(mana.value);
        }

        public float GetMaxMana()
        {
            return Mathf.Round(GetComponent<BaseStats>().GetStat(Stat.Mana));
        }

        public float GetRegenRate()
        {
            return Mathf.Round(GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate));
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana.value)
            {
                return false;
            }

            mana.value -= manaToUse;
            return true;
        }
    }
}