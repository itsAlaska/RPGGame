using System.Collections;
using System.Collections.Generic;
using RPG.Dialogue;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        private PlayerConversant playerConversant;
        [SerializeField] private TextMeshProUGUI AIText;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            AIText.text = playerConversant.GetText();
        }
    }
}