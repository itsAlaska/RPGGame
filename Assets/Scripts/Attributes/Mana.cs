using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {
        LazyValue<float> mana;
        private LazyValue<float> regenRate;

        private Dictionary<string, float> stuffToSave = new Dictionary<string, float>();

        private void Awake()
        {
            mana = new LazyValue<float>(GetMaxMana);
            regenRate = new LazyValue<float>(GetRegenRate);
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
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate);
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

        public object CaptureState()
        {
            return mana.value;
            // stuffToSave["mana"] = mana.value;
            // stuffToSave["regenRate"] = regenRate.value;
            // return stuffToSave;
        }

        public void RestoreState(object state)
        {
            // Dictionary<string, float> stuffToLoad = (Dictionary<string, float>)state;
            mana.value = (float)state;
            // regenRate.value = stuffToLoad["regenRate"];
        }
    }
}