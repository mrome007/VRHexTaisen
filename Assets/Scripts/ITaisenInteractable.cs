using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaisenInteractable
{
    event EventHandler<ActionEventArgs> Success;

    void Interact(bool hover);
}
