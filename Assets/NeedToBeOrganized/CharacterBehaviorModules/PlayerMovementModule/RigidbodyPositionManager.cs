using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPositionManager : MonoBehaviour, IMovementHandler {
    private Rigidbody2D characterRigidBody;

    void Awake(){
        characterRigidBody = GetComponentInParent<Rigidbody2D>();
    }

    public void AddToXPosition(float newXPosition)
    {
        characterRigidBody.position += new Vector2(newXPosition, 0);
    }

    public void AddToYPosition(float newYPosition)
    {
        characterRigidBody.position += new Vector2(0, newYPosition);
    }

}
