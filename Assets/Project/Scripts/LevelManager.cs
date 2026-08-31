using System.Collections;
using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private FadeScreen FadeScreen;
    [SerializeField] private Collider DeathTrigger;
    private bool IsFading;
    private PlayerController Player;

    void Start()
    {
        FadeScreen.FadeToImage();
        Player = FindAnyObjectByType<PlayerController>();

    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneCoroutine());
    }

    private IEnumerator ReloadSceneCoroutine()
    {
        IsFading = true;
        FadeScreen.FadeToBlack();
        yield return new WaitUntil(() => FadeScreen.IsFinished);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
