using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour {
    public event Action BeforeSceneUnload;      //Anything that needs to happen before leaving scene subscribes to this
    public event Action AfterSceneLoad;         //Anything that needs to happen after entering scene subscribes to this
    private bool isFading;                      //Flag to determine if scene is fading
    public float fadeDuration = 1f;             //Duration of load screen fade
    public CanvasGroup faderCanvasGroup;
    public string startingSceneName;            //Name of first scene to be loaded

    private IEnumerator Start()
    {
        faderCanvasGroup.alpha = 1f;
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));
        StartCoroutine(Fade(0f));
    }

    public void FadeAndLoadScene(string sceneName)
    {
        if(!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));      //Fade to black
        if(BeforeSceneUnload != null)
        {
            BeforeSceneUnload();    //Settle anything that needs to be done before scene unload
        }

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        if(AfterSceneLoad != null)
        {
            AfterSceneLoad();       //Settle anything that needs to be done after a scene loads
        }

        yield return StartCoroutine(Fade(0f));      //Fade to new scene
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);    //Additive scene load to load scene on top of persistent scene
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        //While current alpha != approx final alpha, change alpha
        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }
}
