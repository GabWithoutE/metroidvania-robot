using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveSet : MonoBehaviour
{
    public MoveSetNames moveSetName;
    public GameObject meleeAttackHorizontalRight;
    //public GameObject meleeAttackUpRight;
    //public GameObject meleeAttackDownRight;

    public GameObject hammerThrowHorizontalRight;

    public GameObject GetLightAttackHorizontalRight()
    {
        return meleeAttackHorizontalRight;
    }
    
    public GameObject GetHeavyAttackHorizontalRight()
    {
        return hammerThrowHorizontalRight;
    }
    
    public MoveSetNames GetMoveSetName()
    {
        return moveSetName;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}