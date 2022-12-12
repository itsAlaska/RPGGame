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

        void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            yield return GetComponent<_mySavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
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
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
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

        public void Delete()
        {
            GetComponent<_mySavingSystem>().Delete(defaultSaveFile);
        }
    }
}
