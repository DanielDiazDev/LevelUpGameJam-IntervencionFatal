using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Project.Scripts.Music
{
    public class MusicController : MonoBehaviour
    {
        public static MusicController instance;
        [SerializeField] private StudioEventEmitter emitter;

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

        public void SetParameter(string key, float value)
        {
            value = Mathf.Clamp(value, 0, 1);
            
            emitter.SetParameter(key, value);
          
        }

       
    }
}