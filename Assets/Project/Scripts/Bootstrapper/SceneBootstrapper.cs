using System;
using System.Collections;
using Project.Scripts.Timer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadAdditiveScenes());
    }

    private IEnumerator LoadAdditiveScenes()
    {
            var presentOperation = SceneManager.LoadSceneAsync(TimeManager.PRESENT_SCENE, LoadSceneMode.Additive);
            yield return new WaitUntil(() => presentOperation.isDone);
            TimeManager.Instance.RegisterSceneRoot(TimeManager.PRESENT_SCENE);
            var pastOperation = SceneManager.LoadSceneAsync(TimeManager.PAST_SCENE, LoadSceneMode.Additive);
            yield return new WaitUntil(() => pastOperation.isDone);
            TimeManager.Instance.RegisterSceneRoot(TimeManager.PAST_SCENE);
            
            
            
            TimeManager.Instance.SetActiveSceneRoot(TimeManager.PRESENT_SCENE);
            yield return null;
    }
}
