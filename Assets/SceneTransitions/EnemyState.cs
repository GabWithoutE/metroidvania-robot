using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every enemy has these states
[System.Serializable]
public class EnemyState {
    public string name;
    public bool dead;
    [SerializeField]
    public CustomPosition currentPosition;
    [SerializeField]
    public CustomPosition startingPosition;

    public EnemyState()
    {
        name = "";
        dead = true;
        Vector2 tempPosition = new Vector2(-2, -2);
        currentPosition = new CustomPosition(tempPosition);
        startingPosition = new CustomPosition(tempPosition);
    }

    public EnemyState(string nameIn, bool deadIn, Vector2 positionIn)
    {
        name = nameIn;
        dead = deadIn;
        CustomPosition tempCustomPosition = new CustomPosition(positionIn);
        currentPosition = tempCustomPosition;
        startingPosition = tempCustomPosition;
    }

    public string getName()
    {
        return name;
    }

    public bool isDead()
    {
        return dead;
    }

    public CustomPosition getCurrentPosition()
    {
        return currentPosition;
    }
    
    public CustomPosition getStartingPosition()
    {
        return startingPosition;
    }
    
    public void setName(string nameIn)
    {
        name = nameIn;
    }

    public void setDead(bool deadIn)
    {
        dead = deadIn;
    }

    public void setCurrentPosition(Vector2 positionIn)
    {
        currentPosition.setPosition(positionIn);
    }
}
