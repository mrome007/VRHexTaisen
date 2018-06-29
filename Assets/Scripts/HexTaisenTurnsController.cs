using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTaisenTurnsController : MonoBehaviour 
{
    [SerializeField]
    private List<TaisenUnitTurn> unitTurns;

    private int currentUnitTurn;

    public void StartHexTaisen()
    {
        StartUnitTurn();
        currentUnitTurn = 0;
    }

    private void StartUnitTurn()
    {
        var turn = unitTurns[currentUnitTurn];
        turn.TurnStarted += HandleTurnStarted;
        turn.TurnEnded += HandleTurnEnded;
        turn.StartTurn();
    }

    private void HandleTurnEnded(object sender, EventArgs args)
    {
        unitTurns[currentUnitTurn].TurnEnded -= HandleTurnEnded;

        currentUnitTurn++;
        currentUnitTurn %= unitTurns.Count;
        StartUnitTurn();
    }

    private void HandleTurnStarted(object sender, EventArgs args)
    {
        unitTurns[currentUnitTurn].TurnStarted -= HandleTurnStarted;

        //Do stuff at turn start if I want to.
    }

    private void OnDestroy()
    {
        for(int index = 0; index < unitTurns.Count; index++)
        {
            unitTurns[index].TurnEnded -= HandleTurnEnded;
        }
    }
}
