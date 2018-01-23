using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitMenuActions : MonoBehaviour 
{
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
                ShowTaisenUnitMenu(true);
                break;

            case ActionType.AttackUI:
            case ActionType.MoveUI:
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
}
