using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenDragZone : GazeableObject
{
    private TaisenVRCanvas parentPanel;
    private Transform originalParent;

    private void Awake()
    {
        parentPanel = GetComponentInParent<TaisenVRCanvas>();
    }

    public override void OnPress(RaycastHit hit)
    {
        base.OnPress(hit);

        originalParent = parentPanel.transform.parent;
        parentPanel.transform.parent = Camera.main.transform;
    }

    public override void OnRelease(RaycastHit hit)
    {
        base.OnRelease(hit);
        parentPanel.transform.parent = originalParent;
    }
}
