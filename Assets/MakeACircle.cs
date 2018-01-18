using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeACircle : MonoBehaviour 
{
    [SerializeField]
    private Image radialIndicator;

    private float timer = 0f;
    private float progressTime = 1f;


    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            FillUpIndicator();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            timer = 0f;
            radialIndicator.fillAmount = 0f;
        }
    }

    private void FillUpIndicator()
    {
        if(timer < progressTime)
        {
            timer += Time.deltaTime;

            var fillPercentage = timer / progressTime;
            radialIndicator.fillAmount = fillPercentage;
        }
    }
}
