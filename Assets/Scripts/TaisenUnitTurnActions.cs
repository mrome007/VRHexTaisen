using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TaisenUnit))]
public class TaisenUnitTurnActions : MonoBehaviour 
{
    public event EventHandler ActionBegin;
    public event EventHandler ActionComplete;

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
        PostActionBegin();
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.EnableHexCollider(false));
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.HighlightHexTile(false));
        unit.SetOccupiedTile(interactable.GetComponent<HexTile>());

        Debug.Log("Move Action");

        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(MoveConclusion());
    }

    private IEnumerator MoveConclusion()
    {
        yield return null;
        PostActionComplete();

        Debug.Log("End Move Action");
    }

    private void PostActionComplete()
    {
        var handler = ActionComplete;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    private void PostActionBegin()
    {
        var handler = ActionBegin;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
