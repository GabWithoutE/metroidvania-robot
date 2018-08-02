using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState {
    public string name;
    public object state;

    public ObjectState(string stateName, object stateObject)
    {
        name = stateName;
        this.state = stateObject;
    }

    public object GetStateValue()
    {
        return this.state;
    }

    public void SetState(object newState)
    {
        this.state = newState;
    }
}
