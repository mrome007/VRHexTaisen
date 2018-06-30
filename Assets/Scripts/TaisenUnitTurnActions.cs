using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TaisenUnit))]
public class TaisenUnitTurnActions : MonoBehaviour 
{
    public event EventHandler<TurnActionEventArgs> ActionBegin;
    public event EventHandler<TurnActionEventArgs> ActionComplete;

    private TaisenUnit unit;

    private void Awake()
    {
        unit = GetComponent<TaisenUnit>();
    }

    public void TurnActionInteraction(ActionType act, GameObject interactable)
    {
        switch(act)
        {
            case ActionType.MoveAction:
                StartCoroutine(MoveAction(interactable));
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

    private IEnumerator MoveAction(GameObject interactable)
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
