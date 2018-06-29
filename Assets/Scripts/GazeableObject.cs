using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour 
{
    [SerializeField]
    protected ActionType actionType;

    //TODO add more events if needed.
    public event EventHandler<ActionEventArgs> Pressed;
    public event EventHandler<ActionEventArgs> Released;

    public virtual void OnGazeEnter(RaycastHit hit)
    {
    }

    public virtual void OnGaze(RaycastHit hit)
    {
    }

    public virtual void OnGazeExit()
    {
    }

    public virtual void OnPress(RaycastHit hit)
    {
        var handler = Pressed;
        if(handler != null)
        {
            handler(this, new ActionEventArgs(actionType, gameObject));
        }
    }

    public virtual void OnHold(RaycastHit hit)
    {
    }

    public virtual void OnRelease(RaycastHit hit)
    {
        var handler = Released;
        if(handler != null)
        {
            handler(this, new ActionEventArgs(actionType, gameObject));
        }
    }
}
