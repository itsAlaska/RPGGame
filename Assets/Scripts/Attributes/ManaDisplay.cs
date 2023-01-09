using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana;

        void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>();
        }

        void Update()
        {
            GetComponent<TMP_Text>().text =
                $"{mana.GetMana()} / {mana.GetMaxMana()}";
        }
    }
}