using RPG.Control;
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
    }
}

