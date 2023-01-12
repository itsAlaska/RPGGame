using System.Collections;
using RPG.Attributes;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private Transform respawnLocation;
        [SerializeField] private float respawnDelay = 3;
        [SerializeField] private float fadeTime = .2f;
        [SerializeField] private float healthRegenPercentage = 20;

        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn);
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
            
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
            Health health = GetComponent<Health>();
            health.Heal(health.GetMaxHitPoints() * healthRegenPercentage / 100);
            yield return fader.FadeIn(fadeTime);
        }
    }
}