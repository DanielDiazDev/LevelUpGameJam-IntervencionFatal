using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        public static DialogueSystem instance;
        [SerializeField] private List<string> dialogues;

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
        }
    }
}