﻿using System.Collections;
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
    private ICharacterStateManager stateManager;    //Player's state manager
    private CharacterState disableMoveState;        //State to disable player movement while screen is fading

    private IEnumerator Start()
    {
        AfterSceneLoad += UnhideMinimapSectionWhenVisit;
        faderCanvasGroup.alpha = 1f;
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));
        yield return StartCoroutine(Fade(0f));
        EnablePlayerMovement();
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
        DisablePlayerMovement();
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
        EnablePlayerMovement();
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

    private void DisablePlayerMovement()
    {
        //Gets reference to player's state manager if doesn't exist already. Need to get the reference each time a new
        //scene is loaded        
        stateManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ICharacterStateManager>();
        disableMoveState = stateManager.GetExistingCharacterState(ConstantStrings.DISABLE_MOVE_STATE);        
        disableMoveState.SetState(true);
    }

    private void EnablePlayerMovement()
    {
        //Gets reference to player's state manager if doesn't exist already. Need to get the reference each time a new
        //scene is loaded        
        stateManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ICharacterStateManager>();
        disableMoveState = stateManager.GetExistingCharacterState(ConstantStrings.DISABLE_MOVE_STATE);
        disableMoveState.SetState(false);
    }

    private void UnhideMinimapSectionWhenVisit()
    {
        //Find minimap gameobject
        GameObject minimap = GameObject.FindGameObjectWithTag("Minimap");
        //Finds minimap section corresponding to the newly loaded scene
        try {
            GameObject minimapSection = minimap.transform.Find(SceneManager.GetActiveScene().name).gameObject;
            UnhideMinimapSection unhideminimapSection = minimapSection.GetComponent<UnhideMinimapSection>();
            //Set minimap section as visited
            unhideminimapSection.SetVisited(true);
        } catch(Exception e){
            Debug.LogException(e, this);
        }
  

    }
}
