using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTaisenTurnsController : MonoBehaviour 
{
    [SerializeField]
    private List<TaisenUnitTurn> unitTurns;

    private int currentUnitTurn;

    public void StartHexTaisen()
    {
        StartUnitTurn();
        currentUnitTurn = 0;
    }

    private void StartUnitTurn()
    {
        var turn = unitTurns[currentUnitTurn];
        turn.TurnEnded += HandleTurnEnded;
        turn.StartTurn();
    }

    private void HandleTurnEnded(object sender, System.EventArgs e)
    {
        unitTurns[currentUnitTurn].TurnEnded -= HandleTurnEnded;

        currentUnitTurn++;
        currentUnitTurn %= unitTurns.Count;
        StartUnitTurn();
    }

    private void OnDestroy()
    {
        for(int index = 0; index < unitTurns.Count; index++)
        {
            unitTurns[index].TurnEnded -= HandleTurnEnded;
        }
    }
}
