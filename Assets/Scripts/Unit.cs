using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour 
{
	public event EventHandler<UnitEventArgs> TurnBegin;
	public event EventHandler<UnitEventArgs> TurnComplete;

	public enum UnitMode
	{
		Default,
		Move,
		Attack,
		Harvest,
		Defend
	}

	public UnitMode CurrentUnitMode;
	public HexGrid OccupiedHexGrid;
	public int NumberOfUnitActions;
	public int MaxNumberOfUnitActions;

	public abstract void Move();
	public abstract void Attack();
	public abstract void Harvest();
	public abstract void Defend();

	public virtual void StartTurn()
	{
		var handler = TurnBegin;
		if(handler != null)
		{
			handler(this, new UnitEventArgs(this.gameObject));
		}
	}

	public virtual void EndTurn()
	{
		NumberOfUnitActions = 0;

		var handler = TurnComplete;
		if(handler != null)
		{
			handler(this, new UnitEventArgs(this.gameObject));
		}
	}
}
