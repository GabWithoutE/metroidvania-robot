using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public interface IGameSceneLoader {
    /*
     * Interface for loading scenes that have videos or have recorded character movement, etc.
     */ 
	AsyncOperation LoadGameScene(int sceneBuildIndex, LoadSceneMode mode, System.Action<bool> onSceneLoaded);
	AsyncOperation LoadGameScene(string sceneName, LoadSceneMode mode, System.Action<bool> onSceneLoaded);
}
