using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();

        }

        void Update()
        {
            if (fighter.GetTarget() == null)
            {
                GetComponent<TMP_Text>().text = $"N/A";
                return;
            }

            Health health = fighter.GetTarget();
            GetComponent<TMP_Text>().text = $"{health.GetPercentage()}%";
        }
    }
}
