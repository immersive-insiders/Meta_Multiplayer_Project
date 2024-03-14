using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    //public static SceneTransitionManager sceneInstance;
    
    [SerializeField] private FadeScreen fadeScreen;
   // [SerializeField] private SceneLocomotionManager locomotionManager;

    private AsyncOperation asyncOperation;

    //private void Awake()
    //{
    //    if (sceneInstance == null)
    //    {
    //        sceneInstance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //        Destroy(gameObject);
    //}

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine( GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.FadeDuration && !asyncOperation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        fadeScreen.FadeIn();
        //ApplySceneRelatedSettings(sceneIndex);
    }

    //private void ApplySceneRelatedSettings(int sceneIndex)
    //{
    //    if(sceneIndex == 0)
    //    {
    //        locomotionManager.ApplyPauseControlls();
    //    }
    //    else if(sceneIndex == 1)
    //    {
    //        locomotionManager.ApplyMainSceneControlls();
    //    }


    //}
}
