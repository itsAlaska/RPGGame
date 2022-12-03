using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        float weaponRange = 2f;

        [SerializeField]
        float timeBetweenAttacks = 1f;

        [SerializeField]
        float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = 0;

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead)
                return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();

                AttackBehavior();
            }
        }

        void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeBetweenAttacks <= timeSinceLastAttack)
            {
                // This will trigger the Hit() event.
                GetComponent<Animator>()
                    .SetTrigger(RandomAttackAnim());
                timeSinceLastAttack = 0;
            }
        }

        // Animation event
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        // Custom method to go between two random attack animations
        string RandomAttackAnim()
        {
            int number = Random.Range(0, 2);
            if (number == 0)
            {
                return "attack1";
            }
            else
            {
                return "attack2";
            }
        }
    }
}
