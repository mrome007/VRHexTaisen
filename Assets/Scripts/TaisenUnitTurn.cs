using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitTurn : MonoBehaviour 
{
    public event EventHandler TurnEnded;

    [SerializeField]
    private int numberOfActionsPerTurn = 3;

    private int currentNumberOfActions;

    private void Awake()
    {
        currentNumberOfActions = 0;
    }

    public void StartTurn()
    {
        currentNumberOfActions = 0;
    }

    public void TaisenAct()
    {
        currentNumberOfActions++;
        CheckTurns();
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
