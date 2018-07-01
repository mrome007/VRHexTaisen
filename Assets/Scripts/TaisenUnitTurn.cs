using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TaisenUnitTurnActions))]
[RequireComponent(typeof(TaisenUnit))]
public class TaisenUnitTurn : MonoBehaviour 
{
    public event EventHandler TurnStarted;
    public event EventHandler TurnEnded;
    public TaisenUnit Unit { get { return unit; } }

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
    protected TaisenUnit unit;

    protected virtual void Awake()
    {
        currentNumberOfActions = 0;
        turnActions = GetComponent<TaisenUnitTurnActions>();
        unit = GetComponent<TaisenUnit>();
    }

    public virtual void StartTurn()
    {
        Debug.Log("^^ " + gameObject.name + "'s turn");
        RegisterActionEvents();
        PostStartTurn();
        currentNumberOfActions = 0;
        var canAct = CanTaisenAct(1);
        if(canAct)
        {
            //Do something. TODO AI to pick random actions.
        }
        else
        {
            StartCoroutine(DelayEndTurn());
        }
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

    protected bool CanTaisenAct(int numActions)
    {
        var success = (currentNumberOfActions + numActions) <= numberOfActionsPerTurn;
        return success;
    }

    protected void CommitAction(int numActions)
    {
        currentNumberOfActions += numActions;
    }

    protected virtual void EndTurn()
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

    protected virtual void HandleActionBegin(object sender, TurnActionEventArgs args)
    {
    }

    protected virtual void HandleActionComplete (object sender, TurnActionEventArgs args)
    {
        if(args.IsFinal)
        {
            DelayEndTurn();
            return;
        }
        
        var canAct = CanTaisenAct(1);
        if(canAct)
        {
            ContinueTurn();
        }
        else
        {
            DelayEndTurn();
        }
    }

    protected int GetActionPoints(ActionType actType)
    {
        var tryActPoints = 0;

        switch(actType)
        {
            case ActionType.AttackAction:
                tryActPoints = attackActionPoints;
                break;

            case ActionType.MoveAction:
                tryActPoints = moveActionPoints;
                break;

            case ActionType.CatchAction:
                tryActPoints = catchActionPoints;
                break;

             default:
                break;
        }

        return tryActPoints;
    }
}
