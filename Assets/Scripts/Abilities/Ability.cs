using RPG.Attributes;
using RPG.Core;
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
        [SerializeField] private EffectStrategy[] effectStrategies;
        [SerializeField] private float cooldownTime = 0;
        [SerializeField] private float manaCost = 0;

        public override bool Use(GameObject user)
        {
            Mana mana = user.GetComponent<Mana>();
            if (mana.GetMana() < manaCost) return false;

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            if (cooldownStore.GetTimeRemaining(this) > 0)
            {
                return false;
            }

            AbilityData data = new AbilityData(user);

            var actionScheduler = user.GetComponent<ActionScheduler>();
            actionScheduler.StartAction(data);
            
            targetingStrategy.StartTargeting(data, () => { TargetAcquired(data); });
            return true;
        }

        private void TargetAcquired(AbilityData data)
        {
            if (data.IsCanceled()) return;
            
            Mana mana = data.GetUser().GetComponent<Mana>();
            if (!mana.UseMana(manaCost)) return;

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this, cooldownTime);
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