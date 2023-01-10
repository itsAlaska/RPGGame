using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "New Spawn Target Prefab Effect",
        menuName = "Abilities/Effects/SpawnTargetPrefabEffect")]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] private Transform prefabToSpawn;
        [SerializeField] private float destroyDelay = -1;

        public override void StartEffect(AbilityData data, Action finished)
        {
            if (prefabToSpawn == null) return;
            data.StartCoroutine(Effect(data, finished));
        }

        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instance = Instantiate(prefabToSpawn);
            instance.position = data.GetTargetedPoint();
            if (destroyDelay > 0)
            {
                yield return new WaitForSeconds(destroyDelay);
                Destroy(instance.gameObject);
            }

            finished();
        }
    }
}