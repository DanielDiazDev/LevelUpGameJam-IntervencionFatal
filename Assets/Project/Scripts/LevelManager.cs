using System.Collections;
using Project.Scripts.Music;
using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private FadeScreen FadeScreen;
    [SerializeField] private Collider DeathTrigger;
    [SerializeField] private ColliderBridge ExitPortal;
    private bool IsFading;
    private PlayerController Player;

    void Start()
    {
        FadeScreen.FadeToImage();
        Player = FindAnyObjectByType<PlayerController>();
        ExitPortal.OnChildTriggerEnter += OnPortalTriggerEnter;

    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneCoroutine(SceneManager.GetActiveScene().name));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(ReloadSceneCoroutine("Scene_MainMenu"));
    }

    private void OnPortalTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            MusicController.instance.SetParameter("ENd", 1);
            LoadMainMenu();
        }
    }

    private IEnumerator ReloadSceneCoroutine(string name)
    {
        IsFading = true;
        FadeScreen.FadeToBlack();
        yield return new WaitUntil(() => FadeScreen.IsFinished);
        SceneManager.LoadScene(name);
    }

    void Update()
    {
        if (!IsFading)
        {
            if (Player.transform.position.y < DeathTrigger.transform.position.y)
            {
                ReloadScene();
            }
        }
    }
}
