using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField] bool isHearth;
    [SerializeField] float dragSpeed = 5f;
    [SerializeField] Transform particles;
    [SerializeField] LineRenderer lineRenderer;

    bool pickedUp;

    PlayerHealth playerHealth;
    PlayerManager playerManager;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Start()
    {
        HandleSubsctiptions(true);
    }
    void HandleSubsctiptions(bool subscribe)
    {
        if (subscribe)
        {
            playerManager.SubscribeToImmidiateActions(ReactToRevival, false);
            playerManager.SubscribeToActivateControls(TurnOnParticles, true);
            return;
        }
        playerManager.UnsubscribeFromImmidiateActions(ReactToRevival, false);
        playerManager.UnsubscribeFromActivateControls(TurnOnParticles, true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickedUp)
        {
            return;
        }
        if (isHearth)
        {
            AudioManagerScript.Instance.PlayRandomPitch("Pick up");
            lineRenderer.positionCount = 2;


            playerHealth.AddCollectedHearth();
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, playerHealth.transform.position);
            particles.gameObject.SetActive(false);

            //this.gameObject.SetActive(false);
        }
        pickedUp = true;

    }

    void TurnOnParticles() => particles.gameObject.SetActive(true);

    void ReactToRevival()
    {
        if (pickedUp)
            StartCoroutine(DragItemsToCorpse());
        else
        {
            HandleSubsctiptions(false);
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
            lineRenderer.SetPosition(0, this.transform.position);
            yield return new WaitForEndOfFrame();
        }

        HandleSubsctiptions(false);
        this.gameObject.SetActive(false);

    }
}
