using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitTurnActions : MonoBehaviour 
{
    public event EventHandler<TurnActionEventArgs> ActionBegin;
    public event EventHandler<TurnActionEventArgs> ActionComplete;

    public void TurnActionInteraction(ActionType act, GameObject interactable, TaisenUnit unit)
    {
        switch(act)
        {
            case ActionType.MoveAction:
                StartCoroutine(MoveAction(interactable, unit));
                break;
            /*
            case ActionType.AttackAction:

                break;

            case ActionType.CatchAction:

                break;
            */
            default:
                break;
        }
    }

    private IEnumerator MoveAction(GameObject interactable, TaisenUnit unit)
    {
        PostActionBegin(new TurnActionEventArgs(false));
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.EnableHexCollider(false));
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.HighlightHexTile(false));
        unit.SetOccupiedTile(interactable.GetComponent<HexTile>());

        Debug.Log("Move Action");

        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(MoveConclusion());
    }

    private IEnumerator MoveConclusion()
    {
        yield return null;
        PostActionComplete(new TurnActionEventArgs(false));

        Debug.Log("End Move Action");
    }

    private void PostActionComplete(TurnActionEventArgs args)
    {
        var handler = ActionComplete;
        if(handler != null)
        {
            handler(this, args);
        }
    }

    private void PostActionBegin(TurnActionEventArgs args)
    {
        var handler = ActionBegin;
        if(handler != null)
        {
            handler(this, args);
        }
    }
}
