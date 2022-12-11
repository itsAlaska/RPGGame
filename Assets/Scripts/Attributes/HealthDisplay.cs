using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        void Update()
        {
            GetComponent<TMP_Text>().text =
                $"{health.GetHealthPoints()} / {health.GetMaxHitPoints()}";
        }
    }
}
