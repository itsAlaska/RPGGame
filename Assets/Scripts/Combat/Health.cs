using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        float healthPoints = 100f;

        bool isDead = false;
        public bool IsDead
        {
            get { return isDead; }
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && isDead == false)
            {
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
        }
    }
}
