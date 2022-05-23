using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField] bool isHearth;
    [SerializeField] float dragSpeed = 5f;
    bool pickedUp;
    LineRenderer lineRenderer;
    PlayerHealth playerHealth;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        playerHealth = FindObjectOfType<PlayerHealth>();

    }
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        playerHealth.SubscribeToRevival(ReactToRevival);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickedUp)
        {
            return;
        }
        if (isHearth)
        {
            lineRenderer.positionCount = 2;


            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.AddCollectedHearth();
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, playerHealth.transform.position);
            pickedUp = true;
            //this.gameObject.SetActive(false);
        }
    }

    void ReactToRevival()
    {
        if (pickedUp)
        {
            StartCoroutine(DragItemsToCorpse());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator DragItemsToCorpse()
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = playerHealth.transform.position;
        float percent = 0f;
        while (percent < 1f)
        {
            percent += Time.deltaTime * dragSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, percent);
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);

    }
}
