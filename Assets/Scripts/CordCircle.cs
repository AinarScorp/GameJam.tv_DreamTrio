using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CordCircle : MonoBehaviour
{
    [SerializeField] [Range(0, 50)] float newRadius;
    [SerializeField] [Range(0, 10)] float scaleSpeed;
    [SerializeField] [Range(0, 10f)] float subtractCordAmount = 0f;
    [SerializeField] [Range(0, 10)] float defaultCordLength = 1f;
    [SerializeField] [Range(0, 10)] float sizeToShrinkTo = 1f;
    [SerializeField] [Range(0, 20)] float maxCordLength = 1f;
    [SerializeField] [Range(0f, 10f)] float secondsBeforeShrink = 1f;

    float cordLength = 1f;
    bool autoApplySize;

    [SerializeField] Transform target;
    [SerializeField] SpriteRenderer cirlceRenderer;

    AdjustCollider adjustCollider;
    SpriteRenderer spriteRenderer;

    InterfaceManager inerfaceManager;
    PlayerHealth playerHealth;



    private void Awake()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        inerfaceManager = FindObjectOfType<InterfaceManager>();
        adjustCollider = GetComponent<AdjustCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        cordLength = defaultCordLength;
        SwitchCordCircle(false);
        inerfaceManager?.DisplayNewCordLength(cordLength);

        FindObjectOfType<PlayerManager>().SubscribeToActivateControls(StartCircleShrinking, true);
    }
    public bool AutoApplySize { get => autoApplySize; }


    public void ToggleAutoApplySize() => autoApplySize = !autoApplySize;


    public void ApplyNewSize()
    {
        ApplyNewSize(newRadius);
    }
    public void ApplyNewSize(float value)
    {
        transform.localScale = new Vector3(value, value);

    }
    public IEnumerator EncircleTarget()
    {
        transform.position = target.position;

        SwitchCordCircle(true);
        yield return ExpandCircle();

        newRadius = cordLength; //probably not needed
        cordLength = defaultCordLength;
    }
    public void IncreaseCordLength(float amount)
    {
        cordLength += amount;
        if (cordLength > maxCordLength)
            cordLength = maxCordLength;
        inerfaceManager?.DisplayNewCordLength(cordLength);

    }
    IEnumerator ExpandCircle()
    {
        ApplyNewSize(sizeToShrinkTo);
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(cordLength, cordLength);
        float percent = 0f;

        while (percent < 1f)
        {
            percent += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(startScale, endScale, percent);
            yield return new WaitForEndOfFrame();
        }

    }
    public void StartCircleShrinking()
    {
        StartCoroutine(StartShrinking());
    }
    IEnumerator StartShrinking()
    {

        //Vector3 startScale = transform.localScale;
        //Vector3 endScale = new Vector3(defaultCordLength, defaultCordLength);
        //float percent = 0f;

        //while (percent < 1f)
        //{
        //    percent += Time.deltaTime * scaleSpeed;
        //    transform.localScale = Vector3.Lerp(startScale, endScale, percent);
        //    yield return new WaitForEndOfFrame();
        //}
        yield return new WaitForSeconds(secondsBeforeShrink);
        while (transform.localScale.x > sizeToShrinkTo)
        {
            Vector3 newScale = transform.localScale - new Vector3(subtractCordAmount, subtractCordAmount) * Time.deltaTime;
            transform.localScale = newScale;
            yield return new WaitForEndOfFrame();

        }

        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        playerHealth.Revive();

        SwitchCordCircle(false);
        inerfaceManager?.DisplayNewCordLength(cordLength);
    }

    void SwitchCordCircle(bool turnOn)
    {
        cirlceRenderer.enabled = turnOn;
        adjustCollider.enabled = turnOn;
        spriteRenderer.enabled = turnOn;
    }


}
