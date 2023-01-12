using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] private Transform contentRoot;
        [SerializeField] private GameObject buttonPrefab;

        private void OnEnable()
        {
            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
        }
    }
}