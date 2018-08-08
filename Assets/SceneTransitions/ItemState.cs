using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every item has these states
[System.Serializable]
public class ItemState {
    public string name;
    public bool pickedUp;
    [SerializeField]
    //public Vector2 position;
    public CustomPosition position;

    public ItemState()
    {
        name = "";
        pickedUp = false;
        Vector2 tempPosition = new Vector2(-1, -1);
        position = new CustomPosition(tempPosition);
    }

    public ItemState(string nameIn, Vector2 positionIn)
    {
        name = nameIn;
        pickedUp = false;
        position.setPosition(positionIn);
    }

    public string getName()
    {
        return name;
    }

    public bool isPickedUp()
    {
        return pickedUp;
    }

    public CustomPosition getPosition()
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
        position.setPosition(positionIn);
    }
}
