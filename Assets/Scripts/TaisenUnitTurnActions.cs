using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitTurnActions : MonoBehaviour 
{
    [SerializeField]
    private TaisenUnit unit;

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
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.EnableHexCollider(false));
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.HighlightHexTile(false));
        unit.SetOccupiedTile(interactable.GetComponent<HexTile>());
    }
}
