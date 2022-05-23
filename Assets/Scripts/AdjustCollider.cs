using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(EdgeCollider2D))]
public class AdjustCollider : MonoBehaviour
{
    public float Radius = 1.0f;
    public int NumPoints = 32;

    EdgeCollider2D edgeCollider;
    float CurrentRadius = 0.0f;

    private void Awake()
    {
        if (edgeCollider == null)
        {
            edgeCollider = GetComponent<EdgeCollider2D>();
        }
    }
    void Start()
    {
        CreateCircle();
    }
    private void OnEnable()
    {
        edgeCollider.enabled = true;
    }
    private void OnDisable()
    {
        edgeCollider.enabled = false;

    }

    void Update()
    {
        if (NumPoints != edgeCollider.pointCount || CurrentRadius != Radius)
        {
            CreateCircle();
        }
    }

    void CreateCircle()
    {
        Vector2[] edgePoints = new Vector2[NumPoints + 1];
        if (edgeCollider == null)
            edgeCollider = GetComponent<EdgeCollider2D>();

        for (int loop = 0; loop <= NumPoints; loop++)
        {
            float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
            edgePoints[loop] = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        }

        edgeCollider.points = edgePoints;
        CurrentRadius = Radius;
    }
}
