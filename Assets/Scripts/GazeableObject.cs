using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour 
{
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
    }

    public virtual void OnHold(RaycastHit hit)
    {
        Debug.Log("Button Hold");
    }

    public virtual void OnRelease(RaycastHit hit)
    {
        Debug.Log("Button Released");
    }
}
