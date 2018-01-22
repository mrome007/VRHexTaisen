﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnit : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> UnitActionUiGameObject;

    [SerializeField]
    private GameObject ReturnUiGameObject;

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
                    currentInteractableObject = hit.collider.gameObject;
                    currentInteractableInterface = interactable;
                    currentInteractableInterface.Success += HandleSuccess;
                }
                else if(currentInteractableObject.GetInstanceID() != hit.collider.GetInstanceID())
                {
                    currentInteractableInterface.Success -= HandleSuccess;

                    currentInteractableObject = hit.collider.gameObject;
                    currentInteractableInterface = interactable;
                    currentInteractableInterface.Success += HandleSuccess;
                }

                currentInteractableInterface.Interact(true);
            }
            else
            {
                if(currentInteractableInterface != null)
                {
                    currentInteractableInterface.Success -= HandleSuccess;
                    currentInteractableInterface.Interact(false);
                    
                    currentInteractableObject = null;
                    currentInteractableInterface = null;
                }
            }
        }
        else
        {
            if(currentInteractableInterface != null)
            {
                currentInteractableInterface.Success -= HandleSuccess;
                currentInteractableInterface.Interact(false);

                currentInteractableObject = null;
                currentInteractableInterface = null;
            }
        }
    }

    private void HandleSuccess(object sender, ActionEventArgs e)
    {
        currentInteractableInterface.Success -= HandleSuccess;
        switch(e.actionType)
        {
            case ActionType.MoveUI:
            case ActionType.AttackUI:
                HandleActionMenuSuccess();
                break;

            case ActionType.ReturnUI:
                HandleReturnMenuSuccess();
                break;
            default:
                break;
        }
    }

    private void HandleActionMenuSuccess()
    {
        UnitActionUiGameObject.ForEach(ui => ui.SetActive(false));
        ReturnUiGameObject.SetActive(true);
    }

    private void HandleReturnMenuSuccess()
    {
        ReturnUiGameObject.SetActive(false);
        UnitActionUiGameObject.ForEach(ui => ui.SetActive(true));
    }
}













