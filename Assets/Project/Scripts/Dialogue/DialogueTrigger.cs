using System;
using UnityEngine;

namespace Project.Scripts.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private int index;


        private void OnTriggerEnter(Collider other)
        {
            DialogueSystem.instance.Show(index);
        }

        private void OnTriggerExit(Collider other)
        {
            DialogueSystem.instance.Hide();
        }
    }
}