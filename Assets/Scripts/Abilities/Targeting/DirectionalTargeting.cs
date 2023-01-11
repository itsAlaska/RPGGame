using System;
using RPG.Control;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Directional Targeting", menuName = "Abilities/Targeting/Directional Targeting", order = 0)]
    public class DirectionalTargeting : TargetingStrategy
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float groundOffset = 1;
        public override void StartTargeting(AbilityData data, Action finished)
        {
            var ray = PlayerController.GetMouseRay();
            if (Physics.Raycast(ray, out var raycastHit, 1000, layerMask))
            {
                data.SetTargetedPoint(raycastHit.point + ray.direction * groundOffset / ray.direction.y);
            }

            finished();
        }
    }
}