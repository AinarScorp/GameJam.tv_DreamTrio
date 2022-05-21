using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CordCircle : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    float newRadius;

    bool autoApplySize;


    [SerializeField] Transform target;

    public bool AutoApplySize { get => autoApplySize; }
    public float NewRadius { get => newRadius; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }
    //    if (autoApplySize)
    //    {
    //        transform.localScale = new Vector3(newRadius, newRadius);

    //    }

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " have entered");
    }
    public void ToggleAutoApplySize() => autoApplySize = !autoApplySize;
    public void ApplyNewSize()
    {
        transform.localScale = new Vector3(newRadius, newRadius);

    }
    public void EncircleTarget()
    {
        transform.position = target.position;

    }
}
