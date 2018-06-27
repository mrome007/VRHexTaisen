using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitTurnActions : MonoBehaviour 
{
    public event EventHandler TurnActionSuccess;
    public event EventHandler TurnActionFailed;

    private TaisenUnit unit;
    private TaisenUnitTurn unitTurn;

    private void Awake()
    {
        unit = GetComponent<TaisenUnit>();
        if(unit == null)
        {
            Debug.LogError("Requires a TaisenUnit component.");
        }

        unitTurn = GetComponent<TaisenUnitTurn>();
        if(unitTurn == null)
        {
            Debug.LogError("Requires a TaiseUnitTurn component.");
        }
    }

    public void TurnActionInteraction(ActionType act, GameObject interactable)
    {
        switch(act)
        {
            case ActionType.MoveAction:
                MoveAction(interactable);
                break;
            
            case ActionType.AttackAction:

                break;

            case ActionType.CatchAction:

                break;

            default:
                break;
        }
    }

    private void MoveAction(GameObject interactable)
    {
        var success = unitTurn.TaisenAct(1); //TODO 1 for now.

        PostSuccessfulAction(success);

        if(!success)
        {
            return;
        }

        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.EnableHexCollider(false));
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.HighlightHexTile(false));
        unit.SetOccupiedTile(interactable.GetComponent<HexTile>());
    }

    private void PostSuccessfulAction(bool success)
    {
        var handler = success ? TurnActionSuccess : TurnActionFailed;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
