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
        float timeBetweenAttacks = 1f;

        [SerializeField]
        Transform handTransform = null;

        [SerializeField]
        Weapon defaultWeapon = null;

        Health target;
        Weapon currentWeapon = null;
        float timeSinceLastAttack = Mathf.Infinity;

        void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead)
                return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
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
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger(RandomAttackAnim());
        }

        // Animation event
        void Hit()
        {
            if (target == null)
                return;

            target.TakeDamage(currentWeapon.GetDamage);
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position)
                <= currentWeapon.GetRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
        }

        void TriggerStopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack1");
            GetComponent<Animator>().ResetTrigger("attack2");
            GetComponent<Animator>().SetTrigger("stopAttack");
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

        internal bool CanAttack(CombatTarget target)
        {
            throw new System.NotImplementedException();
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            if (weapon != null)
            {
                weapon.Spawn(handTransform, animator);
            }
        }
    }
}
