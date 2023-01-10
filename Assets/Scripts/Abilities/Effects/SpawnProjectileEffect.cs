using System;
using RPG.Attributes;
using RPG.Combat;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "New Spawn Projectile Effect", menuName = "Abilities/Effects/Spawn Projectile",
        order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] private Projectile projectileToSpawn;
        [SerializeField] private float damage;
        [SerializeField] private bool isRightHand = true;

        public override void StartEffect(AbilityData data, Action finished)
        {
            var fighter = data.GetUser().GetComponent<Fighter>();
            var spawnPosition = fighter.GetHandTransform(isRightHand);
            foreach (var target in data.GetTargets())
            {
                var health = target.GetComponent<Health>();
                if (health)
                {
                    var projectile = Instantiate(projectileToSpawn, spawnPosition);
                    projectile.SetTarget(health, data.GetUser(), damage);
                }
            }

            finished();
        }
    }
}