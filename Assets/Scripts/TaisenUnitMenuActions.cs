using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitMenuActions : TaisenVRCanvas
{
    [SerializeField]
    private List<GameObject> UnitActionUiGameObject;
    
    [SerializeField]
    private GameObject ReturnUiGameObject;

    private ActionType currentAction;

    public void ResetUnitMenu()
    {
        ShowTaisenUnitMenu(true);
    }

    public void UnitMenuInteraction(ActionType act)
    {
        UnitMenuAction(act);
        //Turn Actions
        switch(act)
        {
            case ActionType.AttackAction:
            case ActionType.CatchAction:
            case ActionType.MoveAction:
                UnitMenuAction(currentAction);
                break;
            
            default:
                break;
        }
    }

    private void UnitMenuAction(ActionType act)
    {
        switch(act)
        {
            case ActionType.ReturnUI:
                Reset();
                ShowTaisenUnitMenu(true);
                currentAction = act;
                break;
                
            case ActionType.AttackUI:
                ShowTaisenUnitMenu(false);
                currentAction = act;
                break;
                
            case ActionType.MoveUI:
                EnableMoveElements(true);
                ShowTaisenUnitMenu(false);
                currentAction = act;
                break;
                
            case ActionType.CatchUI:
                ShowTaisenUnitMenu(false);
                currentAction = act;
                break;
                
            default:
                break;
        }
    }

    private void ShowTaisenUnitMenu(bool show)
    {  
        UnitActionUiGameObject.ForEach(menu => menu.SetActive(show));
        ReturnUiGameObject.SetActive(!show);
    }

    private void EnableMoveElements(bool enable)
    {
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.EnableHexCollider(enable));
        unit.OccupiedTile.AdjacentTiles.ForEach(adjTile => adjTile.HighlightHexTile(enable));
    }

    private void Reset()
    {
        EnableMoveElements(false);
    }
}
