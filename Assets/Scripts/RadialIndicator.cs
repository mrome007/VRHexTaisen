using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialIndicator : MonoBehaviour
{
    private static Image radialIndicator;

    private static float timer = 0f;
    private static float progressTime = 1f;

    private void Awake()
    {
        radialIndicator = GetComponent<Image>();
    }

    public static bool FillUpIndicator(bool hover)
    {
        if(hover)
        {
            if(timer < progressTime)
            {
                timer += Time.deltaTime;

                var fillPercentage = timer / progressTime;
                radialIndicator.fillAmount = fillPercentage;
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            timer = 0f;
            radialIndicator.fillAmount = 0f;
            return false;
        }
    }
}
