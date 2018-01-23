using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimedInteractable : MonoBehaviour, ITaisenInteractable
{
    [SerializeField]
    private ActionType actionType;

    public event EventHandler<ActionEventArgs> Success;

    public void Interact(bool hover)
    {
        var success = RadialIndicator.FillUpIndicator(hover);
        if(success)
        {
            var handler = Success;
            handler(this, new ActionEventArgs(actionType));
        }
    }
}
