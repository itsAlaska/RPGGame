using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class _mySavingWrapper : MonoBehaviour
    {
        [SerializeField]
        float fadeInTime = 2f;
        const string defaultSaveFile = "_mySave";

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<_mySavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<_mySavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<_mySavingSystem>().Save(defaultSaveFile);
        }
    }
}
