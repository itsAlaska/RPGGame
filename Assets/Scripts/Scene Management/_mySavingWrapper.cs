using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace RPG.SceneManagement
{
    public class _mySavingWrapper : MonoBehaviour
    {
        private const string currentSaveKey = "currentSaveName";
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] private float fadeOutTime = .2f;
        [SerializeField] private int firstLevelBuildIndex = 1;
        [SerializeField] private int menuLevelBuildIndex = 0;

        public void ContinueGame()
        {
            if (!PlayerPrefs.HasKey(currentSaveKey)) return;
            if (!GetComponent<_mySavingSystem>().SaveFileExists(GetCurrentSave())) return;
            StartCoroutine(LoadLastScene());
        }

        public void NewGame(string saveFile)
        {
            if (string.IsNullOrEmpty(saveFile)) return;
            SetCurrentSave(saveFile);
            StartCoroutine(LoadFirstScene());
        }
        
        public void LoadGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            ContinueGame();
        }

        public void LoadMenu()
        {
            StartCoroutine(LoadMenuScene());
        }

        private void SetCurrentSave(string saveFile)
        {
            PlayerPrefs.SetString(currentSaveKey, saveFile);
        }

        private string GetCurrentSave()
        {
            return PlayerPrefs.GetString(currentSaveKey);
        }

        private IEnumerator LoadFirstScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }
        
        private IEnumerator LoadMenuScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<_mySavingSystem>().LoadLastScene(GetCurrentSave());
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void Load()
        {
            GetComponent<_mySavingSystem>().Load(GetCurrentSave());
        }

        public void Save()
        {
            GetComponent<_mySavingSystem>().Save(GetCurrentSave());
        }

        public void Delete()
        {
            GetComponent<_mySavingSystem>().Delete(GetCurrentSave());
        }

        public IEnumerable<string> ListSaves()
        {
            return GetComponent<_mySavingSystem>().ListSaves();
        }

        
    }
}