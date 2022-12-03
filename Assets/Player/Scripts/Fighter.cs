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

        Transform target;
        float timeSinceLastAttack = 0;

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();

                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if (timeBetweenAttacks <= timeSinceLastAttack)
            {
                GetComponent<Animator>().SetTrigger(RandomAttackAnim());
                timeSinceLastAttack= 0;
            }
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        // Animation event
        void Hit() { }

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
