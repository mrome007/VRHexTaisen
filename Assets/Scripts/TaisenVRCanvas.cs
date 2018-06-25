using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenVRCanvas : MonoBehaviour 
{
    [SerializeField]
    protected TaisenUnit unit;

    private TaisenGazeableButton currentActiveButton;

    private void Update()
    {
        LookAtPlayer();
    }

    public void SetActiveButton(TaisenGazeableButton activeButton)
    {
        if(currentActiveButton != null)
        {
        }

        if(activeButton != null && currentActiveButton != activeButton)
        {
            currentActiveButton = activeButton;
        }
        else
        {
            currentActiveButton = null;
        }
    }

    private void LookAtPlayer()
    {
        var playerPosition = unit.transform.position;
        var vectorPlayer = playerPosition - transform.position;

        var lookAtPos = transform.position - vectorPlayer;
        transform.LookAt(lookAtPos);
    }
}
