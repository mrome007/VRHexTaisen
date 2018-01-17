using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour 
{
	public Transform MainVRPlayer;
	public Unit[] Units;

	private int currentUnitIndex = 0;
	private Unit currentUnit;

	private void Start()
	{
		if(Units.Length <= 0)
		{
			Debug.LogError("No Units. Add Units to start");
		}

		currentUnit = Units[currentUnitIndex];
		currentUnit.TurnBegin += UnitTurnBegin;
		currentUnit.TurnComplete += UnitTurnComplete;
		currentUnit.StartTurn();
	}

	private void UnitTurnBegin(object sender, UnitEventArgs e)
	{
		currentUnit.TurnBegin -= UnitTurnBegin;
		Debug.Log(e.GameObject.name + " starts its turn");

		MainVRPlayer.transform.position = new Vector3(currentUnit.transform.position.x, -1.5f, currentUnit.transform.position.z);
		MainVRPlayer.transform.parent = currentUnit.transform;
	}

	private void UnitTurnComplete(object sender, UnitEventArgs e)
	{
		currentUnit.TurnComplete -= UnitTurnComplete;
		Debug.Log(e.GameObject.name + " ends its turn");
		MainVRPlayer.transform.parent = null;
		NextUnit();

		InitializeCurrentUnit();
	}

	private void InitializeCurrentUnit()
	{
		currentUnit = Units[currentUnitIndex];
		currentUnit.TurnBegin += UnitTurnBegin;
		currentUnit.TurnComplete += UnitTurnComplete;
		currentUnit.StartTurn();
	}

	private void NextUnit()
	{
		currentUnitIndex++;
		currentUnitIndex %= Units.Length;
	}
		
	private IEnumerator Turns()
	{
		while(true)
		{
			var unitPosition = Units[currentUnitIndex].gameObject.transform.position;
			MainVRPlayer.transform.position = new Vector3(unitPosition.x, -1.5f, unitPosition.z);
			currentUnitIndex++;
			currentUnitIndex %= Units.Length;
			yield return new WaitForSeconds(5f);
		}
	}
}
