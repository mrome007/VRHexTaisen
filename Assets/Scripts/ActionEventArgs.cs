using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventArgs : EventArgs 
{
    public ActionType ActionType { get; private set; }
    public GameObject Interactable { get; private set; }

    public ActionEventArgs(ActionType actType, GameObject interact)
    {
        ActionType = actType;
        Interactable = interact;
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
