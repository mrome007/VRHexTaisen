using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TaisenInteractable : MonoBehaviour, IGvrPointerHoverHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public event EventHandler Success;

    public virtual void OnGvrPointerHover(PointerEventData eventData)
    {
        RadialIndicator.FillUpIndicator(true);
    }
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        RadialIndicator.FillUpIndicator(true);
    }
    
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        RadialIndicator.FillUpIndicator(false);
    }
}
