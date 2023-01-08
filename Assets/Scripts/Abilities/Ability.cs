using System.Collections.Generic;
using RPG.Inventories;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] private TargetingStrategy targetingStrategy;
        [SerializeField] private FilterStrategy[] filterStrategies;
        [SerializeField] private EffectStrategy[] effectStrategies;

        public override void Use(GameObject user)
        {
            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data, () => { TargetAcquired(data); });
        }

        private void TargetAcquired(AbilityData data)
        {
            foreach (var filterStrategy in filterStrategies)
            {
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {
        }
    }
}