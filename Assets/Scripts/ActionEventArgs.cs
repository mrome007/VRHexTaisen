using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventArgs : EventArgs 
{
    public ActionType actionType { get; private set; }

    public ActionEventArgs(ActionType actType)
    {
        actionType = actType;
    }
}

public enum ActionType
{
    Default,
    ReturnUI,
    MoveUI,
    AttackUI,
    CatchUI,
    MoveAction,
    AttackAction,
    CatchAction,
    SeeInfo
}
