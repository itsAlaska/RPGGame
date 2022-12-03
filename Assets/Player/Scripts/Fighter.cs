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

        Transform target;

        void Update()
        {
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

                // Adding this in, not part of the course
                // Randomizes attack animation
                // AttackAnimation();
            }
        }

        private void AttackBehavior()
        {
            GetComponent<Animator>().SetTrigger("attack1");
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

        // Adding this in, not part of the course
        // Randomizes attack animation
        // void AttackAnimation()
        // {
        //     int attackAnim = Random.Range(1, 3);
        //     Animator animator = GetComponent<Animator>();
        //     switch (attackAnim)
        //     {
        //         case 1:
        //             animator.SetTrigger("attack1");
        //             break;
        //         case 2:
        //             animator.SetTrigger("attack2");
        //             break;
        //     }
        // }
    }
}
