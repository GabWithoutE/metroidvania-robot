using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every enemy has these states
[System.Serializable]
public class EnemyState {
    public string name;
    public bool alive;
    [SerializeField]
    public Vector2 position;

    public EnemyState()
    {
        name = "";
        alive = true;
        position = new Vector2(-1, -1);
    }

    public EnemyState(string nameIn, Vector2 positionIn)
    {
        name = nameIn;
        alive = true;
        position = positionIn;
    }

    public string getName()
    {
        return name;
    }

    public bool isAlive()
    {
        return alive;
    }

    public Vector2 getPosition()
    {
        return position;
    }

    public void setName(string nameIn)
    {
        name = nameIn;
    }

    public void setAlive(bool aliveIn)
    {
        alive = aliveIn;
    }

    public void setPosition(Vector2 positionIn)
    {
        position = positionIn;
    }
}
