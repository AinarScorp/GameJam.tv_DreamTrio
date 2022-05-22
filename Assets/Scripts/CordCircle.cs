using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CordCircle : MonoBehaviour
{
    [HideInInspector][SerializeField][Range(0,50)] 
    float newRadius;

    [HideInInspector] [SerializeField] 
    Transform target;
    [SerializeField] [Range(0, 10)] float scaleSpeed;
    [SerializeField] [Range(0, 2)] float subtractCordAmount = 2f;

    [HideInInspector][SerializeField] [Range(0, 20)]
    float defaultCordLength = 1f;

    [SerializeField] float cordLength = 1f;
    bool autoApplySize;

    Coroutine shrinkingCorotine;

    UI_Manager uI_Manager;
    private void Awake()
    {
        uI_Manager = FindObjectOfType<UI_Manager>();
    }
    private void Start()
    {
        cordLength = defaultCordLength;
        uI_Manager.DisplayNewCordLength(cordLength);
    }
    public bool AutoApplySize { get => autoApplySize; }
    public float NewRadius { get => newRadius; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " have entered");
    }
    public void ToggleAutoApplySize() => autoApplySize = !autoApplySize;
    public void ApplyNewSize()
    {
        transform.localScale = new Vector3(newRadius, newRadius);

    }
    public void EncircleTarget(Transform newTarget = null)
    {
        this.gameObject.SetActive(true);

        newRadius = cordLength;
        cordLength = defaultCordLength;
        ApplyNewSize();
        if (newTarget != null)
        {
            target = newTarget;
        }
        transform.position = target.position;
        StartCircleShrinking();
    }
    public void IncreaseCordLength(float amount)
    {
        cordLength += amount;
        uI_Manager.DisplayNewCordLength(cordLength);

    }

    public void StartCircleShrinking()
    {
        shrinkingCorotine = StartCoroutine(StartShrinking());
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

        while (transform.localScale.x > defaultCordLength)
        {
            Vector3 newScale = transform.localScale - new Vector3(subtractCordAmount, subtractCordAmount);
            transform.localScale = newScale;
            yield return new WaitForEndOfFrame();

        }



        this.gameObject.SetActive(false);
    }
}