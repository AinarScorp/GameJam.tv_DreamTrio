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

    PlayerManager playerManager;

    private void Awake()
    {
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
            return;
        pickedUp = true;


        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, playerManager.Player.transform.position);
        AudioManagerScript.Instance.PlayRandomPitch("Pick up");

        if (isHearth)
            playerManager.AddCollectedHearth();
        else
            playerManager.AddCollectedFireBall();

    }

    void TurnOnParticles() => particles.gameObject.SetActive(true);

    void ReactToRevival()
    {
        if (pickedUp)
            StartCoroutine(DragItemsToCorpse());
        else
        {
            HandleSubsctiptions(false);
            DestroyMe();
        }
    }

    IEnumerator DragItemsToCorpse()
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = playerManager.Player.transform.position;
        float percent = 0f;
        while (percent < 1f)
        {
            percent += Time.deltaTime * dragSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, percent);
            lineRenderer.SetPosition(0, this.transform.position);
            yield return new WaitForEndOfFrame();
        }

        HandleSubsctiptions(false);
        DestroyMe();
    }
    void DestroyMe()
    {

        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 3);
    }
}
