using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class _mySavingWrapper : MonoBehaviour
    {
        [SerializeField]
        float fadeInTime = 2f;

        [SerializeField]private float fadeOutTime = .2f;
        const string defaultSaveFile = "_mySave";
        
        public void ContinueGame()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
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
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
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
