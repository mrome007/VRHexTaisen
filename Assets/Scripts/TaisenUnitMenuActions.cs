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
        ShowTaisenUnitMenuScan(true);
    }

    public void UnitMenuInteraction(ActionType act)
    {
        UnitMenuAction(act);
    }

    private void UnitMenuAction(ActionType act)
    {
        switch(act)
        {
            case ActionType.ReturnUI:
                Reset();
                ShowTaisenUnitMenuScan(true);
                currentAction = act;
                break;
                
            case ActionType.AttackUI:
                ShowTaisenUnitMenuScan(false);
                currentAction = act;
                break;
                
            case ActionType.MoveUI:
                EnableMoveElements(true);
                ShowTaisenUnitMenuScan(false);
                currentAction = act;
                break;
                
            case ActionType.CatchUI:
                ShowTaisenUnitMenuScan(false);
                currentAction = act;
                break;
                
            default:
                break;
        }
    }

    private void ShowTaisenUnitMenuScan(bool show)
    {  
        UnitActionUiGameObject.ForEach(menu => menu.SetActive(show));
        ReturnUiGameObject.SetActive(!show);
    }

    public void ShowTaisenUnitMenu(bool show)
    {
        UnitActionUiGameObject.ForEach(menu => menu.SetActive(show));
        ReturnUiGameObject.SetActive(show);
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
