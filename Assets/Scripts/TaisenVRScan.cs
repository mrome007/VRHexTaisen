using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenVRScan : TaisenUnitTurn
{
    [SerializeField]
    private TaisenUnitMenuActions unitMenu;

    [SerializeField]
    private GameObject taisenReticle;

    [SerializeField]
    private Color originalColor;

    [SerializeField]
    private Color gazeColor;

    private Vector3 midScreen;
    private GazeableObject currentGazeObject;
    private GazeableObject currentSelectableObject;
    private RaycastHit lastHit;
    private Coroutine taisenScanRoutine;

    protected override void Awake()
    {
        base.Awake();
        midScreen = new Vector3(0.5f, 0.5f, 0f);
        SetReticleColor(originalColor);
        taisenScanRoutine = null;
    }

    public override void StartTurn()
    {
        Debug.Log("^^ " + gameObject.name + "'s turn");
        RegisterActionEvents();
        PostStartTurn();
        currentNumberOfActions = 0;
        unitMenu.ShowTaisenUnitMenu(true);
        var canAct = CanTaisenAct(1);
        if(canAct)
        {
            BeginPlayerScan();
        }
        else
        {
            EndTurn();
        }
    }

    protected override void EndTurn()
    {
        base.EndTurn();
        EndPlayerScan();
    }

    protected override void ContinueTurn()
    {
        base.ContinueTurn();
        BeginPlayerScan();
        unitMenu.ShowTaisenUnitMenu(true);
    }

    private void BeginPlayerScan()
    {
        if(taisenScanRoutine == null)
        {
            taisenScanRoutine = StartCoroutine(VRScan());
        }
    }

    private void EndPlayerScan()
    {
        if(taisenScanRoutine != null)
        {
            StopCoroutine(taisenScanRoutine);
            taisenScanRoutine = null;
        }
    }

    private IEnumerator VRScan()
    {
        while(true)
        {
            CheckSurroundings();
            CheckForInput(lastHit);
            yield return null;
        }
    }

    private void CheckSurroundings()
    {
        RaycastHit hit;
        var ray = Camera.main.ViewportPointToRay(midScreen);
        if(Physics.Raycast(ray, out hit, 50f))
        {
            var hitObject = hit.collider.gameObject;
            var gaze = hitObject.GetComponent<GazeableObject>();
            if(gaze != null)
            {
                if(gaze != currentGazeObject)
                {
                    ClearCurrentObject();
                    AssignCurrentObject(gaze);
                    gaze.OnGazeEnter(hit);
                    SetReticleColor(gazeColor);
                }
                else
                {
                    currentGazeObject.OnGaze(hit);
                }
            }
            else
            {
                ClearCurrentObject();
            }

            lastHit = hit;
        }
        else
        {
            ClearCurrentObject();
        }
    }

    private void AssignCurrentObject(GazeableObject gaze)
    {
        currentGazeObject = gaze;
        currentGazeObject.Pressed += HandleGazeablePressed;
    }

    private void ClearCurrentObject()
    {
        if(currentGazeObject != null)
        {
            currentGazeObject.OnGazeExit();
            SetReticleColor(originalColor);
            currentGazeObject.Pressed -= HandleGazeablePressed;
            currentGazeObject = null;
        }
    }

    private void SetReticleColor(Color reticleColor)
    {
        var renderer = taisenReticle.GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.material.SetColor("_Color", reticleColor);
        }
    }

    private void CheckForInput(RaycastHit hit)
    {
        if(Input.GetMouseButtonDown(0) && currentGazeObject != null)
        {
            currentSelectableObject = currentGazeObject;
            currentSelectableObject.OnPress(hit);
        }

        if(Input.GetMouseButtonDown(0) && currentSelectableObject != null)
        {
            currentSelectableObject.OnHold(hit);
        }

        if(Input.GetMouseButtonUp(0) && currentSelectableObject != null)
        {
            currentSelectableObject.OnRelease(hit);
            currentSelectableObject = null;
        }
    }

    private void HandleGazeablePressed(object sender, ActionEventArgs actionArgs)
    {
        var points = GetActionPoints(actionArgs.ActionType);
        if(CanTaisenAct(points))
        {
            CommitAction(points);
            turnActions.TurnActionInteraction(actionArgs.ActionType, actionArgs.Interactable);
            unitMenu.UnitMenuInteraction(ActionType.ReturnUI);
        }
        if(points == 0)
        {
            unitMenu.UnitMenuInteraction(actionArgs.ActionType);
        }
    }

    protected override void HandleActionBegin(object sender, System.EventArgs e)
    {
        base.HandleActionBegin(sender, e);
        EndPlayerScan();
        unitMenu.ShowTaisenUnitMenu(false);
    }

    protected override void HandleActionComplete(object sender, System.EventArgs e)
    {
        var canAct = CanTaisenAct(1);
        if(canAct)
        {
            ContinueTurn();
        }
        else
        {
            EndTurn();
        }
    }
}













