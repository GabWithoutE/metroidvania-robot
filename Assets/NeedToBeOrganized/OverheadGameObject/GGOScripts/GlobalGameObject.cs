using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Usage: Get the Global Game Object
 *  IInterface interface = GetComponent(typeof(IInterface)) as IInterface;
 * 
 * TODO: Make a game object for transitioning scenes under certain conditions
 *  (These are added to GameObjects in the menu and in the game that need to use scene
 *  transitions)
 */ 

public class GlobalGameObject : MonoBehaviour, 
IMainSceneChanger, IGameSceneLoader, IUILoadingChanger, ICameraShaker {
    private SceneLoader sceneLoader;
    private TransitionalUIDisplayer transitionalUIDisplayer;
    public string mainGameScene;
    public string titleScreenScene;
	public GameObject loadingScreen;
	public GameObject playerObservant;
	public GameObject camera;
	public static GlobalGameObject globalGameObjectInstance= null;
    
    void Awake(){
		if (globalGameObjectInstance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
			globalGameObjectInstance = this;
        }

        sceneLoader = GetComponentInChildren<SceneLoader>();
        transitionalUIDisplayer = GetComponentInChildren<TransitionalUIDisplayer>();
	}
    /*
     * Interface implementation for loading the game and loading the title Screen
     */ 
    public void StartMainGame(){

		GameObject loadingScreenInstance = transitionalUIDisplayer.DisplayUIElement(loadingScreen);

		AsyncOperation asyncScene = sceneLoader.LoadGameScene(mainGameScene, LoadSceneMode.Single, null);

		playerObservant.SetActive(true);
		asyncScene.allowSceneActivation = true;
        
		Destroy(loadingScreenInstance);

        // NEED TO FIGURE OUT WHAT I WANNA DO AOUT THE API, SINCE I PROBABLY DONT NEED THE CALLBACK NOW SINCE I NEED THE ASYNC SCENE THING.
        // Need to eventually figure out what to do with the showing loading progress maybe.
    }

    public void LoadTitleScreen(){
		GameObject loadingScreenInstance = transitionalUIDisplayer.DisplayUIElement(loadingScreen);

		AsyncOperation asyncScene = sceneLoader.LoadGameScene(titleScreenScene, LoadSceneMode.Single, null); 

		asyncScene.allowSceneActivation = true;
		Destroy(loadingScreenInstance);
    }

    /*
     * Interface implementation for loading the cut scenes, etc. 
     */ 
	public AsyncOperation LoadGameScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single, System.Action<bool> onSceneLoaded = null)
    {
		return sceneLoader.LoadGameScene(sceneName, sceneMode, onSceneLoaded);
    }

	public AsyncOperation LoadGameScene(int sceneBuildIndex, LoadSceneMode sceneMode = LoadSceneMode.Single, System.Action<bool> onSceneLoaded = null)
    {
		return sceneLoader.LoadGameScene(sceneBuildIndex, sceneMode, onSceneLoaded);
    }

    /*
     * For the purpose of loading large chunks of the map without a loading screen transition
     * TODO: probably surround the scene with a large game object that has a collider.
     *  When this when this collider is exited, delete that scene's large gameobject.
     */ 
    public void SmoothTransitionLoadGameScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        sceneLoader.SmoothTransitionLoadGameScene(sceneName, sceneMode);
    }

    public void SmoothTransitionLoadGameScene(int sceneBuildIndex, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        sceneLoader.SmoothTransitionLoadGameScene(sceneBuildIndex, sceneMode);
    }

    /*
     * Interface implementation for displaying certain UI elements for the changes of 
     * scene or game context
     */ 
    public void DisplayUIElement(GameObject UIElement){
        transitionalUIDisplayer.DisplayUIElement(UIElement);
    }

	public void ShakeCameraForSeconds(float seconds){
		camera.GetComponent<CameraShake>().ShakeForSeconds(seconds);
	}


}
