using System.Collections.Generic;
using RPG.Inventories;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] private TargetingStrategy targetingStrategy;
        [SerializeField] private FilterStrategy[] filterStrategies;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user, TargetAcquired);
        }

        private void TargetAcquired(IEnumerable<GameObject> targets)
        {
            foreach (var filterStrategy in filterStrategies)
            {
                targets = filterStrategy.Filter(targets);
            }
            
            
            foreach (var target in targets)
            {
                Debug.Log(target);
            }
        }
    }
}
