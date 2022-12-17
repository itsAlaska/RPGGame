using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField]
        AnimatorOverrideController animatorOverride = null;

        [SerializeField]
        Weapon equippedPrefab = null;

        [SerializeField]
        float weaponDamage = 5f;

        [SerializeField]
        float percentageBonus = 0f;

        [SerializeField]
        float weaponRange = 2f;

        [SerializeField]
        bool isRightHanded = true;

        [SerializeField]
        Projectile projectile = null;

        const string weaponName = "Weapon";

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Weapon weapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);

                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            var overrideControlle =
                animator.runtimeAnimatorController as AnimatorOverrideController;

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideControlle != null)
            {
                animator.runtimeAnimatorController = overrideControlle.runtimeAnimatorController;
            }

            return weapon;
        }

        void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null)
                return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;

            if (isRightHanded)
                handTransform = rightHand;
            else
                handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(
            Transform rightHand,
            Transform leftHand,
            Health target,
            GameObject instigator,
            float calculateDamage
        )
        {
            Projectile projectileInstance = Instantiate(
                projectile,
                GetTransform(rightHand, leftHand).position,
                Quaternion.identity
            );

            projectileInstance.SetTarget(target, instigator, calculateDamage);
        }

        public float GetDamage
        {
            get { return weaponDamage; }
        }

        public float GetPercentageBonus
        {
            get { return percentageBonus; }
        }
        public float GetRange
        {
            get { return weaponRange; }
        }
    }
}
