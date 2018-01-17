using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
	private PlayerUnitMoveMode playerUnitMoveMode;
	private int unitModeLayerMask;

	private float chooseUnitModeTimer = 0f;
	private float confirmUnitModeTime = 2f;

	#region Unit Overrides

	public override void Move()
	{
		NumberOfUnitActions++;
		//Thinking of moving this to PlayerUnitMoveMode script.
		var position = OccupiedHexGrid.transform.position;
		transform.position = new Vector3(position.x, -2.5f, position.z);
		StartCoroutine(BreakAfterMove());
	}

	public override void Attack()
	{
		throw new System.NotImplementedException();
	}

	public override void Harvest()
	{
		throw new System.NotImplementedException();
	}

	public override void Defend()
	{
		throw new System.NotImplementedException();
	}

	public override void StartTurn()
	{
		base.StartTurn();

		if(OccupiedHexGrid == null)
		{
			Debug.Log("Unit does not have hex grid attached");
			StartCoroutine(TemporaryTurn());
		}
		else
		{
			CurrentUnitMode = UnitMode.Default; 
		}
	}

	#endregion

	private void Awake()
	{
		playerUnitMoveMode = GetComponent<PlayerUnitMoveMode>();
		if(playerUnitMoveMode == null)
		{
			Debug.LogError("No player movement component");
		}
	}

	private void Start()
	{
		if(OccupiedHexGrid == null)
		{
			Debug.Log("Unit is not occupying a hex grid.");
			return;
		}

		unitModeLayerMask = (1 << LayerMask.NameToLayer("Move")) | (1 << LayerMask.NameToLayer("Attack")) | 
							(1 << LayerMask.NameToLayer("Harvest")) | (1 << LayerMask.NameToLayer("Defend")) |
							(1 << LayerMask.NameToLayer("Back"));
	}

	private void Update()
	{
		if(CurrentUnitMode == UnitMode.Default)
		{
			ChooseUnitModeHandler();
		}
	}

	private IEnumerator TemporaryTurn()
	{
		yield return new WaitForSeconds(5f);
		EndTurn();
	}

	private IEnumerator BreakAfterMove()
	{
		yield return new WaitForSeconds(5f);
		CheckEndOfTurn();
	}

	private void CheckEndOfTurn()
	{
		if(NumberOfUnitActions >= MaxNumberOfUnitActions)
		{
			EndTurn();
		}
		else
		{
			CurrentUnitMode = UnitMode.Default; 
		}
	}

	private void ChooseUnitModeHandler()
	{
		var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 10f, unitModeLayerMask))
		{
			chooseUnitModeTimer += Time.deltaTime;
			if(chooseUnitModeTimer >= confirmUnitModeTime)
			{
				chooseUnitModeTimer = 0f;
				SetUnitMode(hit.collider.tag);
			}

		}
		else
		{
			chooseUnitModeTimer = 0f;
		}
	}

	private void SetUnitMode(string mode)
	{
		switch(mode)
		{
			case "Move":
				Debug.Log("Set unit mode to MOVE");
				CurrentUnitMode = UnitMode.Move;
				playerUnitMoveMode.StartPlayerMovement();
				break;

			case "Attack":
				CurrentUnitMode = UnitMode.Attack;
				break;

			case "Harvest":
				CurrentUnitMode = UnitMode.Harvest;
				break;

			case "Defend":
				CurrentUnitMode = UnitMode.Defend;
				break;

			default:
				break;
		}
	}
}
