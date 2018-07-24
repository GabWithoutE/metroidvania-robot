using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastingBehavior : MonoBehaviour {
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    
    void Awake()
    {
        Instantiate(attack1, transform);
        Instantiate(attack2, transform);
        Instantiate(attack3, transform);
    }

    public GameObject GetAttack(int index)
    {
        if(index == 1)
        {
            return attack1;
        }
        else if(index == 2)
        {
            return attack2;
        }
        return attack3;
    }
}
