using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTaisenTurnsController : MonoBehaviour 
{
    [SerializeField]
    private TaisenParty players;

    [SerializeField]
    private TaisenParty enemies;

    private int currentUnitTurn;
    private List<TaisenUnitTurn> unitTurns;

    private void Awake()
    {
        unitTurns = new List<TaisenUnitTurn>();
        ArrangeTurns();
    }

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

        Debug.Log("Starting " + turn.gameObject.name + "'s Turn.");
    }

    private void HandleTurnEnded(object sender, EventArgs args)
    {
        Debug.Log("Ending " + unitTurns[currentUnitTurn].gameObject.name + "'s Turn.");

        unitTurns[currentUnitTurn].TurnEnded -= HandleTurnEnded;

        if(players.Dead || enemies.Dead)
        {
            EndTurns();
            return;
        }

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

    private void ArrangeTurns()
    {
        foreach(var turn in players)
        {
            unitTurns.Add(turn);
        }

        foreach(var turn in enemies)
        {
            unitTurns.Add(turn);
        }
    }

    public void EndTurns()
    {
        //TODO DO SOMETHING HERE WHEN ONE OF THE PARTY DIES.
    }
}
