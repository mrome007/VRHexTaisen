using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaisenGazeableButton : GazeableObject
{
    protected TaisenVRCanvas parentPanel;

    private void Awake()
    {
        parentPanel = GetComponentInParent<TaisenVRCanvas>();
    }

    public override void OnPress(RaycastHit hit)
    {
        base.OnPress(hit);
        if(parentPanel != null)
        {
            parentPanel.SetActiveButton(this);
        }
        else
        {
            Debug.LogError("Button not a child of object with VRPanel component.", this);
        }
    }

    public void SetButtonColor(Color buttonColor)
    {
        GetComponent<Image>().color = buttonColor;
    }
}
