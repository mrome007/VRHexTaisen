using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACircle : MonoBehaviour 
{
    [SerializeField]
    private LineRenderer circle;

    [SerializeField]
    private float radius;

    private float timer = 5f;
    private float theta;
    private Vector3[] points;
    private float increment;
    private int index;
    private void Start()
    {
        theta = 0f;
        points = new Vector3[120];
        increment = (2f * Mathf.PI) / timer;
        index = 0;
    }

    private void Update()
    {
        MakeCircle();
    }

    private void MakeCircle()
    {
        if(index < points.Length)
        {
            theta += increment * Time.deltaTime;

            var x = radius * Mathf.Cos(theta);
            var y = radius * Mathf.Sin(theta);
            index++;
            circle.positionCount = index;
            circle.SetPosition(index - 1, new Vector3(x, y, 0f));
        }
    }
}
