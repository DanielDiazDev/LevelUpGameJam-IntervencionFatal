using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        public static DialogueSystem instance;
        [SerializeField] private List<string> dialogues;
        [SerializeField] private GameObject dialoguePrefab;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            Hide();
        }

        public void Show(int index)
        {
            dialoguePrefab.SetActive(true);
            dialogueText.text = dialogues[index];
        }

        public void Hide()
        {
            dialoguePrefab.SetActive(false);
            dialogueText.text = "";
        }
    }
}