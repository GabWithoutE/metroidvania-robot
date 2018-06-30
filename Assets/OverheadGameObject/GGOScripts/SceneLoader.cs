using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* TODO: Maybe make a class to associate scenes with a particular type of loading screen?
 * 
 */ 

public class SceneLoader : MonoBehaviour {
	private AsyncOperation asyncLoad;
	public AsyncOperation LoadGameScene(int sceneBuildIndex, LoadSceneMode sceneMode, System.Action<bool> onSceneLoaded){
		asyncLoad = null;
		StartCoroutine(LoadAsyncScene(sceneBuildIndex, sceneMode, onSceneLoaded));
		return asyncLoad;
	}

	public AsyncOperation LoadGameScene(string sceneName, LoadSceneMode sceneMode, System.Action<bool> onSceneLoaded) {
		asyncLoad = null;
		StartCoroutine(LoadAsyncScene(sceneName, sceneMode, onSceneLoaded));
		return asyncLoad;
	}

    public void SmoothTransitionLoadGameScene (string sceneName, LoadSceneMode sceenMode){
        /*
         * Need to figure out how to implement this eventually
         */ 
    }

    public void SmoothTransitionLoadGameScene(int sceneBuildIndex, LoadSceneMode sceenMode)
    {
        /*
         * Need to figure out how to implement this eventually
         */ 
    }


    IEnumerator LoadAsyncScene(int sceneBuildIndex, LoadSceneMode sceneMode, System.Action<bool> onSceneLoaded){
        asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, sceneMode);
		asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone){
            yield return null;
        }

		if (onSceneLoaded != null)
        {
            onSceneLoaded(true);
        }
		yield return asyncLoad;

    }

	IEnumerator LoadAsyncScene(string sceneName, LoadSceneMode sceneMode, System.Action<bool> onSceneLoaded){
		asyncLoad = SceneManager.LoadSceneAsync(sceneName, sceneMode);
		asyncLoad.allowSceneActivation = false;

		while (!asyncLoad.isDone){
			yield return null;
		}

		//yield return new WaitForSeconds(3);
		if (onSceneLoaded != null){
			onSceneLoaded(true);
		}

		yield return asyncLoad;

	}
}
