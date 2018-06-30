using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PersistBetweenScenes : MonoBehaviour {
    private static bool created = false;

    void Awake(){
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
	}
}
