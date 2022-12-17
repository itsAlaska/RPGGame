using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField]
        TMP_Text[] damageDigits = null;

        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float amount)
        {
            // string convertedAmount = Mathf.RoundToInt(amount).ToString();
            string convertedAmount = string.Format("{0:0}", amount);
            int digits = convertedAmount.Length;

            switch (digits)
            {
                case 1:
                    TextSetter(1, convertedAmount);
                    break;
                case 2:
                    TextSetter(2, convertedAmount);
                    break;
                case 3:
                    TextSetter(3, convertedAmount);
                    break;
                case 4:
                    TextSetter(4, convertedAmount);
                    break;
                default:
                    break;
            }
        }

        void TextSetter(int numberLength, string convertedAmount)
        {
            char[] convertedToChar = convertedAmount.ToCharArray();

            for (int i = 0; i < numberLength; i++)
            {
                damageDigits[i].text = convertedToChar[i].ToString();
            }
        }
    }
}
