using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Inventories;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] Transform rightHandTransform = null;

        [SerializeField] Transform leftHandTransform = null;

        [SerializeField] WeaponConfig defaultWeapon = null;

        Health target;
        Equipment equipment;
        WeaponConfig currentWeaponConfig;
        LazyValue<Weapon> currentWeapon;
        float timeSinceLastAttack = Mathf.Infinity;

        void Awake()
        {
            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }

        void Start()
        {
            currentWeapon.ForceInit();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead)
                return;

            if (!GetIsInRange(target.transform))
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

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }

            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(
                    rightHandTransform,
                    leftHandTransform,
                    target,
                    gameObject,
                    damage
                );
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        void Shoot()
        {
            Hit();
        }

        bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position)
                   <= currentWeaponConfig.GetRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            if (
                !GetComponent<Mover>().CanMoveTo(combatTarget.transform.position)
                && !GetIsInRange(combatTarget.transform)
            )
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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus;
            }
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

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig;
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }

        Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        public Transform GetHandTransform(bool isRightHand)
        {
            if (isRightHand) return rightHandTransform;
            return leftHandTransform;
        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }
    }
}