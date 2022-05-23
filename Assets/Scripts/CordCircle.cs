using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CordCircle : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    [Range(0, 50)]
    float newRadius;

    [Tooltip("Target to encircle")]
    [SerializeField] Transform target;
    [SerializeField] [Range(0, 10)] float scaleSpeed; //not used right now
    [SerializeField] [Range(0, 20f)] float subtractCordAmount = 0f;



    [SerializeField][Range(0, 20)] float defaultCordLength = 1f;

    [SerializeField][Range(0, 20)] float sizeToShrinkTo = 1f;
    [SerializeField] [Range(0, 500)] float maxCordLength = 1f;

    [SerializeField] float cordLength = 1f;
    bool autoApplySize;


    UI_Manager uI_Manager;
    private void Awake()
    {
        uI_Manager = FindObjectOfType<UI_Manager>();
    }
    private void Start()
    {
        cordLength = defaultCordLength;
        uI_Manager?.DisplayNewCordLength(cordLength);
        SwitchCordCircle(false);
        FindObjectOfType<PlayerHealth>().SubscribeToPlayerDeathPermanently(EncircleTarget);
    }
    public bool AutoApplySize { get => autoApplySize; }
    public float NewRadius { get => newRadius; }


    public void ToggleAutoApplySize() => autoApplySize = !autoApplySize;
    public void ApplyNewSize()
    {
        transform.localScale = new Vector3(newRadius, newRadius);

    }
    public void EncircleTarget()
    {
        SwitchCordCircle(true);

        newRadius = cordLength;
        cordLength = defaultCordLength;
        ApplyNewSize();

        transform.position = target.position;
        StartCircleShrinking();
    }
    public void IncreaseCordLength(float amount)
    {
        cordLength += amount;
        uI_Manager.DisplayNewCordLength(cordLength);
        if (cordLength > maxCordLength)
        {
            cordLength = maxCordLength;
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

        while (transform.localScale.x > sizeToShrinkTo)
        {
            Vector3 newScale = transform.localScale - new Vector3(subtractCordAmount, subtractCordAmount) * Time.deltaTime;
            transform.localScale = newScale;
            yield return new WaitForEndOfFrame();

        }

        FindObjectOfType<PlayerHealth>().Revive();

        SwitchCordCircle(false);
        uI_Manager.DisplayNewCordLength(cordLength);
    }

    void SwitchCordCircle(bool turnOn)
    {
        GetComponent<AdjustCollider>().enabled = turnOn;
        GetComponent<SpriteRenderer>().enabled = turnOn;
    }


}
