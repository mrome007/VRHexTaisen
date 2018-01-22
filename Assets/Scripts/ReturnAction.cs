using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnAction : MonoBehaviour, ITaisenInteractable
{
    public event EventHandler<ActionEventArgs> Success;
    
    public void Interact(bool hover)
    {
        var success = RadialIndicator.FillUpIndicator(hover);
        if(success)
        {
            var handler = Success;
            handler(this, new ActionEventArgs(ActionType.ReturnUI));
        }
    }
}
