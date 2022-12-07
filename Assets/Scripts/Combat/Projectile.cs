using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        float speed = 1;

        Health target = null;
        float damage = 0;

        void Update()
        {
            if (target == null)
                return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)
                return;

            target.TakeDamage(damage);
            Destroy(gameObject);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            print($"{target.name} is being targeted");
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
                return target.transform.position;

            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }
    }
}
