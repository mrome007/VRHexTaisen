using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenVRScan : MonoBehaviour 
{
    [SerializeField]
    private TaisenUnitMenuActions unitMenu;

    [SerializeField]
    private TaisenUnitTurnActions turnActions;

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

    private void Awake()
    {
        midScreen = new Vector3(0.5f, 0.5f, 0f);
        SetReticleColor(originalColor);
    }

    private void Update()
    {
        CheckSurroundings();
        CheckForInput(lastHit);
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
            currentGazeObject.Pressed += HandleGazeablePressed;
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
        turnActions.TurnActionInteraction(actionArgs.ActionType, actionArgs.Interactable);
        unitMenu.UnitMenuInteraction(actionArgs.ActionType);
    }
}













