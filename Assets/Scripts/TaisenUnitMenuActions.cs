using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitMenuActions : MonoBehaviour 
{
    [SerializeField]
    private TaisenUnit unit;

    [SerializeField]
    private List<GameObject> UnitActionUiGameObject;
    
    [SerializeField]
    private GameObject ReturnUiGameObject;

    public void ResetUnitMenu()
    {
        ShowTaisenUnitMenu(true);
    }

    public void UnitMenuInteraction(ActionType act)
    {
        switch(act)
        {
            case ActionType.ReturnUI:
                Reset();
                ShowTaisenUnitMenu(true);
                break;

            case ActionType.AttackUI:
                ShowTaisenUnitMenu(false);
                break;

            case ActionType.MoveUI:
                EnableMoveElements(true);
                ShowTaisenUnitMenu(false);
                break;

            case ActionType.CatchUI:
                ShowTaisenUnitMenu(false);
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
