using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(EdgeCollider2D))]
public class AdjustCollider : MonoBehaviour
{
    public float Radius = 1.0f;
    public int NumPoints = 32;

    EdgeCollider2D EdgeCollider;
    float CurrentRadius = 0.0f;


    void Start()
    {
        CreateCircle();
    }


    void Update()
    {
        // If the radius or point count has changed, update the circle
        if (NumPoints != EdgeCollider.pointCount || CurrentRadius != Radius)
        {
            CreateCircle();
        }
    }

    void CreateCircle()
    {
        Vector2[] edgePoints = new Vector2[NumPoints + 1];
        EdgeCollider = GetComponent<EdgeCollider2D>();

        for (int loop = 0; loop <= NumPoints; loop++)
        {
            float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
            edgePoints[loop] = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        }

        EdgeCollider.points = edgePoints;
        CurrentRadius = Radius;
    }
}
