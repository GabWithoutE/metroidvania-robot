using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState {
    public string name;
    public bool pickedUp;
    public Vector2 position;

    public ItemState()
    {
        name = "";
        pickedUp = false;
        position = new Vector2(-1, -1);
    }

    public ItemState(string nameIn, Vector2 positionIn)
    {
        name = nameIn;
        pickedUp = false;
        position = positionIn;
    }

    public string getName()
    {
        return name;
    }

    public bool isPickedUp()
    {
        return pickedUp;
    }

    public Vector2 getPosition()
    {
        return position;
    }

    public void setName(string nameIn)
    {
        name = nameIn;
    }

    public void setPickedUp(bool pickedUpIn)
    {
        pickedUp = pickedUpIn;
    }

    public void setPosition(Vector2 positionIn)
    {
        position = positionIn;
    }
}
