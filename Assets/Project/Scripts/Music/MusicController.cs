using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Project.Scripts.Music
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private EventReference musicEvent;
        private EventInstance musicInstance;

        private void Create()
        {
            musicInstance = RuntimeManager.CreateInstance(musicEvent);
            musicInstance.start();
        }

        public void SetParameter(string key, float value)
        {
          musicInstance.setParameterByName(key, value);   
        }

        private void OnDestroy()
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
    }
}