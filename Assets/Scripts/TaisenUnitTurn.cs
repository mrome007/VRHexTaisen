using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TaisenUnitTurnActions))]
public class TaisenUnitTurn : MonoBehaviour 
{
    public event EventHandler TurnStarted;
    public event EventHandler TurnEnded;

    [SerializeField]
    protected int numberOfActionsPerTurn = 3;

    [SerializeField]
    protected int currentNumberOfActions;

    [SerializeField]
    protected int moveActionPoints = 1;

    [SerializeField]
    protected int attackActionPoints = 2;

    [SerializeField]
    protected int catchActionPoints = 2;

    protected TaisenUnitTurnActions turnActions;

    protected virtual void Awake()
    {
        currentNumberOfActions = 0;
        turnActions = GetComponent<TaisenUnitTurnActions>();
    }

    public virtual void StartTurn()
    {
        Debug.Log("^^ " + gameObject.name + "'s turn");
        RegisterActionEvents();
        PostStartTurn();
        currentNumberOfActions = 0;
        TaisenAct(0);
    }

    protected virtual void RegisterActionEvents()
    {
        turnActions.ActionBegin += HandleActionBegin;
        turnActions.ActionComplete += HandleActionComplete;
    }

    protected virtual void UnRegisterActionEvents()
    {
        turnActions.ActionBegin -= HandleActionBegin;
        turnActions.ActionComplete -= HandleActionComplete;
    }

    protected virtual void ContinueTurn()
    {

    }

    protected bool TaisenAct(int numActions)
    {
        var success = (currentNumberOfActions + numActions) <= numberOfActionsPerTurn;
        if(success)
        {
            currentNumberOfActions += numActions;
        }
        return success;
    }

    protected virtual void CheckTurns()
    {
        if(currentNumberOfActions >= numberOfActionsPerTurn)
        {
            //PostEndTurn()
            StartCoroutine(DelayEndTurn());
        }
    }

    public virtual void EndTurn()
    {
        UnRegisterActionEvents();
        PostEndTurn();
    }

    protected IEnumerator DelayEndTurn()
    {
        yield return new WaitForSeconds(5f);
        EndTurn();
    }

    protected void PostEndTurn()
    {
        var handler = TurnEnded;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    protected void PostStartTurn()
    {
        var handler = TurnStarted;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    protected virtual void HandleActionBegin(object sender, EventArgs e)
    {
    }

    protected virtual void HandleActionComplete (object sender, EventArgs e)
    {
    }
}
