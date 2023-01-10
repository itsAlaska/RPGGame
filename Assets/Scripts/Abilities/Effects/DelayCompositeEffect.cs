using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "New Delay Composite Effect", menuName = "Abilities/Effects/Delay Composite",
        order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] private float delay = 0;
        [SerializeField] private EffectStrategy[] delayedEffects;
        
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffect(data, finished));
        }

        private IEnumerator DelayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay);
            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished);
            }
        }
    }
}