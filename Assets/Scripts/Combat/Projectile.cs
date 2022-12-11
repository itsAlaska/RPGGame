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

        [SerializeField]
        bool isHoming = true;

        [SerializeField]
        GameObject hitEffect = null;

        [SerializeField]
        float maxLifeTime = 10f;

        [SerializeField]
        GameObject[] destroyOnHit = null;

        [SerializeField]
        float lifeAfterImpact = 2f;

        Health target = null;
        float damage = 0;

        void Start()
        {
            if (target == null) return;
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null)
                return;

            if (isHoming && !target.IsDead)
                transform.LookAt(GetAimLocation());

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)
                return;

            if (target.IsDead)
            {
                return;
            }

            target.TakeDamage(damage);
            speed = 0;
            GameObject hit = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            hit.transform.LookAt(GameObject.FindWithTag("Player").transform.position);

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, lifeAfterImpact);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
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
