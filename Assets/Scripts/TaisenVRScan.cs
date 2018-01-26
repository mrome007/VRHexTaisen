using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenVRScan : MonoBehaviour 
{
    [SerializeField]
    private TaisenUnitMenuActions unitMenu;

    [SerializeField]
    private TaisenUnitTurnActions turnActions;

    private GameObject currentInteractableObject = null;
    private ITaisenInteractable currentInteractableInterface = null;
    private Vector3 midScreen;

    private void Awake()
    {
        midScreen = new Vector3(0.5f, 0.5f, 0f);
    }

    private void Update()
    {
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        RaycastHit hit;
        var ray = Camera.main.ViewportPointToRay(midScreen);
        if(Physics.Raycast(ray, out hit, 50f))
        {
            var interactable = hit.collider.gameObject.GetComponent<ITaisenInteractable>();
            if(interactable != null)
            {
                if(currentInteractableObject == null)
                {
                    AssignInteractable(hit, interactable);
                }
                else if(currentInteractableObject.GetInstanceID() != hit.collider.GetInstanceID())
                {
                    currentInteractableInterface.Success -= HandleScanSuccess;
                    AssignInteractable(hit, interactable);
                }
                
                currentInteractableInterface.Interact(true);
            }
            else
            {
                if(currentInteractableInterface != null)
                {
                    ResetInteractable();
                }
            }
        }
        else
        {
            if(currentInteractableInterface != null)
            {
                ResetInteractable();
            }
        }
    }

    private void HandleScanSuccess(object sender, ActionEventArgs e)
    {
        currentInteractableInterface.Success -= HandleScanSuccess;
        unitMenu.UnitMenuInteraction(e.ActionType);
        turnActions.TurnActionInteraction(e.ActionType, e.Interactable);
    }

    private void ResetInteractable()
    {
        currentInteractableInterface.Success -= HandleScanSuccess;
        currentInteractableInterface.Interact(false);
        
        currentInteractableObject = null;
        currentInteractableInterface = null;
    }

    private void AssignInteractable(RaycastHit hit, ITaisenInteractable interactable)
    {
        currentInteractableObject = hit.collider.gameObject;
        currentInteractableInterface = interactable;
        currentInteractableInterface.Success += HandleScanSuccess;
    }
}













