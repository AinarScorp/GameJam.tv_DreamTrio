using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField] bool isHearth;

    LineRenderer lineRenderer;


    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(this.name);

        if (isHearth)
        {
            lineRenderer.positionCount = 2;


            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.AddCollectedHearth();
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, playerHealth.transform.position);
            //this.gameObject.SetActive(false);
        }
    }
}
