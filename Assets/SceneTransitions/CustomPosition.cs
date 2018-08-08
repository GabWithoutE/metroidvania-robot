using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomPosition {
    private float horizontal;
    private float vertical;

    public CustomPosition(Vector2 positionIn)
    {
        horizontal = positionIn.x;
        vertical = positionIn.y;
    }

    public void setPosition(Vector2 positionIn)
    {
        horizontal = positionIn.x;
        vertical = positionIn.y;
    }

    public float getHorizontal()
    {
        return horizontal;
    }

    public float getVertical()
    {
        return vertical;
    }

    public Vector2 getVect2()
    {
        Vector2 temp;
        temp.x = horizontal;
        temp.y = vertical;
        return temp;
    }
}
