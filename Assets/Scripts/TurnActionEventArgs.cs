using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionEventArgs : EventArgs 
{
    public bool IsFinal { get; private set; }

    public TurnActionEventArgs(bool final)
    {
        IsFinal = final;
    }
}
