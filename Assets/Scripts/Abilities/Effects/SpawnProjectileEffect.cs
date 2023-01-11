using System;
using Gaia;
using RPG.Attributes;
using RPG.Combat;
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
        [SerializeField] private bool useTargetPoint = true;

        public override void StartEffect(AbilityData data, Action finished)
        {
            var fighter = data.GetUser().GetComponent<Fighter>();
            var spawnPosition = fighter.GetHandTransform(isRightHand).position;
            if (useTargetPoint)
            {
                SpawnProjectileForTargetPoint(data, spawnPosition);
            }
            else
            {
                SpawnProjectilesForTargets(data, spawnPosition);
            }
            
            finished();
        }

        private void SpawnProjectileForTargetPoint(AbilityData data, Vector3 spawnPosition)
        {
            var projectile = Instantiate(projectileToSpawn);
            projectile.transform.position = spawnPosition;
            projectile.SetTarget(data.GetTargetedPoint(), data.GetUser(), damage);
        }

        private void SpawnProjectilesForTargets(AbilityData data, Vector3 spawnPosition)
        {
            foreach (var target in data.GetTargets())
            {
                var health = target.GetComponent<Health>();
                if (health)
                {
                    var projectile = Instantiate(projectileToSpawn);
                    projectile.transform.position = spawnPosition;
                    projectile.SetTarget(health, data.GetUser(), damage);
                }
            }
        }
    }
}