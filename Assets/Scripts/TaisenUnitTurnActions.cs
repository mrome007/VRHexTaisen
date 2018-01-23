using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnitTurnActions : MonoBehaviour 
{
    [SerializeField]
    private TaisenUnit unit;

    public void TurnActionInteraction(ActionType act)
    {
        switch(act)
        {
            case ActionType.MoveAction:

                break;
            
            case ActionType.AttackAction:

                break;

            case ActionType.CatchAction:

                break;

            default:
                break;
        }
    }
}
