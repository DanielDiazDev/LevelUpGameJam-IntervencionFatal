using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Music;
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
        
        [SerializeField] private Color originalTileColor;
        [SerializeField] private Color newTileColor;
        private void OnDestroy()
        {
            if (RenderSettings.skybox != null)
            {
                RenderSettings.skybox.SetColor("_Tint", originalSkyTint);
            }
            UpdateTilesColor(originalTileColor);
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
            UpdateTilesColor(originalTileColor);
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
                        UpdateTilesColor(originalTileColor);
                    }
                    MusicController.instance.SetParameter("Pasado", 0);
                }
                else
                {
                    if (RenderSettings.skybox != null)
                    {
                        originalSkyTint = RenderSettings.skybox.GetColor("_Tint");

                        RenderSettings.skybox.SetColor("_Tint", skyTint);
                        UpdateTilesColor(newTileColor);
                        MusicController.instance.SetParameter("Pasado", 1);
                    }
                }
            }
            if (timer <= 0 && !canChangeTime)
            {
                canChangeTime = true;

            }
        }
        private void UpdateTilesColor(Color targetColor)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            foreach (var tile in tiles)
            {
                var renderer = tile.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.color = targetColor;
                }
            }
        }

        private IEnumerator GeneratePortal()
        {
            var go = Instantiate(portalPrefab, 
                new Vector3(player.position.x, player.position.y + 1.5f, player.position.z - -0.87967f), 
                Quaternion.identity);

            Transform portalTransform = go.transform;
            portalTransform.localScale = Vector3.zero;

            float duration = 0.3f; 
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                portalTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 2f, t);
                elapsed += Time.deltaTime;
                yield return null;
            }
            portalTransform.localScale = Vector3.one * 2f;

            elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                portalTransform.localScale = Vector3.Lerp(Vector3.one * 2f, Vector3.zero, t);
                elapsed += Time.deltaTime;
                yield return null;
            }
            portalTransform.localScale = Vector3.zero;

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