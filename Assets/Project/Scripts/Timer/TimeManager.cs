using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Timer
{
    public class TimeManager : MonoBehaviour
    {
        public const string PRESENT_SCENE = "Scene_Present";
        public const string PAST_SCENE = "Scene_Past";
        public static TimeManager Instance;
        private Dictionary<string, GameObject> sceneRoots = new Dictionary<string, GameObject>();
        [SerializeField] private Transform objectAffectedTime;
        public Transform ObjectAffectedTime => objectAffectedTime;
        private int currentIndex;
        public TimeState CurrentState { get; private set; }
        private float timer;
        [SerializeField] private float maxTimer = 2;
        private bool canChangeTime;
        [SerializeField]private Color originalSkyTint = new Color(128, 128, 128);
        [SerializeField] private Color skyTint = Color.gray;
        [SerializeField] private GameObject portalPrefab;
        [SerializeField] private Transform player;

        private void OnDestroy()
        {
            if (RenderSettings.skybox != null)
            {
                RenderSettings.skybox.SetColor("_Tint", originalSkyTint);
            }
        }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if (RenderSettings.skybox != null)
            {
                RenderSettings.skybox.SetColor("_Tint", originalSkyTint);
            }
            timer = maxTimer;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                canChangeTime = true;

            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame && canChangeTime)
            {
                StartCoroutine(GeneratePortal());
                canChangeTime = false;
                currentIndex = (currentIndex + 1) % sceneRoots.Count;
                SetActiveSceneRoot(sceneRoots.ElementAt(currentIndex).Key);
                timer = maxTimer;
                if (currentIndex == 0)
                {
                    if (RenderSettings.skybox != null)
                    {
                        RenderSettings.skybox.SetColor("_Tint", originalSkyTint);
                    }
                }
                else
                {
                    if (RenderSettings.skybox != null)
                    {
                        originalSkyTint = RenderSettings.skybox.GetColor("_Tint");

                        RenderSettings.skybox.SetColor("_Tint", skyTint);
                    }
                }
            }
            if (timer <= 0 && !canChangeTime)
            {
                canChangeTime = true;

            }
        }

        private IEnumerator GeneratePortal()
        {
            var go = Instantiate(portalPrefab, new Vector3(player.position.x, player.position.y + 1.5f, player.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            Destroy(go);
        }
        public void RegisterSceneRoot(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.IsValid())
            {
                foreach (var go in scene.GetRootGameObjects())
                {
                    if (go.name == "Root")
                    {
                        sceneRoots[sceneName] = go;
                        go.SetActive(false);
                    }
                }
            }
        }
        public void SetActiveSceneRoot(string sceneName)
        {
            foreach (var kvp in sceneRoots)
            {
                kvp.Value.SetActive(kvp.Key == sceneName);
            }
            CurrentState = sceneName == PAST_SCENE ? TimeState.Past : TimeState.Present;
            NotifyChangeState(CurrentState);
        }
        private void NotifyChangeState(TimeState state)
        {
            foreach (var changeState in objectAffectedTime.transform.GetComponentsInChildren<IChangeState>())
            {
                changeState.ChangeState(state);
            }
        }
    }

    public interface IChangeState
    {
        void ChangeState(TimeState currentState);
    }

    public enum TimeState
    {
        Past,
        Present
    }
}