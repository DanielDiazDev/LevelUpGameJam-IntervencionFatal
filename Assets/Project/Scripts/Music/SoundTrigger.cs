using UnityEngine;

namespace Project.Scripts.Music
{
    public class SoundTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MusicController.instance.SetParameter("Avance", 1);
            }
        }
    }
}