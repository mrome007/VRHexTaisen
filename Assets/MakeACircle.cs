using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACircle : MonoBehaviour 
{
    [SerializeField]
    private LineRenderer circle;

    [SerializeField]
    private float radius;

    private float timer = 1.5f;
    private float theta;
    private List<Vector3> points;
    private float increment;

    private void Start()
    {
        theta = 0f;
        points = new List<Vector3>();
        increment = (2f * Mathf.PI) / timer;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            MakeCircle();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            theta = 0f;
            circle.positionCount = 0;
            points.Clear();
        }
    }

    private void MakeCircle()
    {
        if(theta < 2f * Mathf.PI)
        {
            var x = radius * Mathf.Cos(theta);
            var y = radius * Mathf.Sin(theta);
            points.Add(new Vector3(x, y, 0f));
            circle.positionCount = points.Count;

            theta += increment * Time.deltaTime;

            if(theta >= 2f * Mathf.PI)
            {
                points.Add(new Vector3(radius, 0.05f, 0f));
                circle.positionCount = points.Count;
            }

            circle.SetPositions(points.ToArray());
        }
    }
}
