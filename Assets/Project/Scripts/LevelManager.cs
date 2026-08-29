using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private FadeScreen FadeScreen;

    void Start()
    {
        FadeScreen.FadeToImage();
    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneCoroutine());
    }

    private IEnumerator ReloadSceneCoroutine()
    {
        FadeScreen.FadeToBlack();
        yield return new WaitUntil(() => FadeScreen.IsFinished);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
