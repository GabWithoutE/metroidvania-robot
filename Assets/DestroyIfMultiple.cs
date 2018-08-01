using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfMultiple : MonoBehaviour {
    void Awake()
    {
        //If there is already an instance of a scene manager, desroy this one
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
