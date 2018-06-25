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
        Debug.Log("Gaze entered on " + gameObject.name);
    }

    public virtual void OnGaze(RaycastHit hit)
    {
        Debug.Log("Gaze hold on " + gameObject.name);
    }

    public virtual void OnGazeExit()
    {
        Debug.Log("Gaze exited on " + gameObject.name);
    }

    public virtual void OnPress(RaycastHit hit)
    {
        Debug.Log("Button Press");
        var handler = Pressed;
        if(handler != null)
        {
            handler(this, new ActionEventArgs(actionType, gameObject));
        }
    }

    public virtual void OnHold(RaycastHit hit)
    {
        Debug.Log("Button Hold");
    }

    public virtual void OnRelease(RaycastHit hit)
    {
        Debug.Log("Button Released");
        var handler = Released;
        if(handler != null)
        {
            handler(this, new ActionEventArgs(actionType, gameObject));
        }
    }
}
