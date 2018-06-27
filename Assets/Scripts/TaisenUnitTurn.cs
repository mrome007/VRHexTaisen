using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitTurn : MonoBehaviour 
{
    public event EventHandler TurnEnded;

    [SerializeField]
    private int numberOfActionsPerTurn = 3;

    [SerializeField]//TODO temporary so I can see in the inspector.
    private int currentNumberOfActions;

    private void Awake()
    {
        currentNumberOfActions = 0;
    }

    public void StartTurn()
    {
        currentNumberOfActions = 0;
        CheckTurns();
    }

    public bool TaisenAct(int numActions)
    {
        var success = (currentNumberOfActions + numActions) <= numberOfActionsPerTurn;
        if(success)
        {
            currentNumberOfActions += numActions;
        }

        CheckTurns();

        return success;
    }

    private void CheckTurns()
    {
        if(currentNumberOfActions >= numberOfActionsPerTurn)
        {
            var handler = TurnEnded;
            if(handler != null)
            {
                handler(this, null);
            }
        }
    }
}
