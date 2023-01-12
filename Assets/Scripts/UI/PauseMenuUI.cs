using RPG.Control;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        private PlayerController playerController;
        private void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            playerController.enabled = false;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            playerController.enabled = true;
        }

        public void Save()
        {
            var savingWrapper = FindObjectOfType<_mySavingWrapper>();
            savingWrapper.Save();
        }

        public void SaveAndQuit()
        {
            var savingWrapper = FindObjectOfType<_mySavingWrapper>();
            savingWrapper.Save();
            savingWrapper.LoadMenu();
        }
    }
}

