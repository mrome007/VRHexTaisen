using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitMoveMode : MonoBehaviour 
{

	private PlayerUnit playerUnit;
	private GameObject CurrentMoveObject = null;
	private float moveTimer = 0f;
	private float confirmMoveTime = 1f;

	public void StartPlayerMovement()
	{
		CurrentMoveObject = null;
		SetAdjacentGridLayers(playerUnit.OccupiedHexGrid, "AdjacentHexGrid");
	}

	private void Awake()
	{
		playerUnit = GetComponent<PlayerUnit>();
		if(playerUnit == null)
		{
			Debug.LogError("No Player Unit");
		}
	}

	private void Update()
	{
		if(playerUnit.CurrentUnitMode == Unit.UnitMode.Move)
		{
			CheckForMove();
		}
	}

	private void CheckForMove()
	{
		var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		var layerMask = 1 << LayerMask.NameToLayer("AdjacentHexGrid"); //Only check for collisions with AdjacentHexGrid.
		if(Physics.Raycast(ray, out hit, 10f, layerMask))
		{
			if(CurrentMoveObject == null)
			{
				CurrentMoveObject = hit.collider.gameObject;
			}
			else
			{
				if(CurrentMoveObject.GetInstanceID() == hit.collider.gameObject.GetInstanceID())
				{
					Debug.Log("The Same");
					moveTimer += Time.deltaTime;
					CheckConfirmMove();
				}
				else
				{
					Debug.Log("Not The Same");
					moveTimer = 0f;
					CurrentMoveObject = null;
				}
			}
		}
		else
		{
			moveTimer = 0f;
		}
	}

	private void CheckConfirmMove()
	{
		if(moveTimer >= confirmMoveTime)
		{
			Debug.Log("Confirm Move");
			moveTimer = 0f;
			SetAdjacentGridLayers(playerUnit.OccupiedHexGrid, "Default");
			playerUnit.OccupiedHexGrid = CurrentMoveObject.GetComponent<HexGrid>();
			playerUnit.Move();
		}
	}

	private void SetAdjacentGridLayers(HexGrid hex, string layer)
	{
		for(int hexIndex = 0; hexIndex < hex.AdjacentHexGrids.Length; hexIndex++)
		{
			hex.AdjacentHexGrids[hexIndex].gameObject.layer = LayerMask.NameToLayer(layer);
		}
	}
}
