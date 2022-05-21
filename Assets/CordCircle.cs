using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CordCircle : MonoBehaviour
{
    [HideInInspector][SerializeField][Range(0,50)] 
    float newRadius;

    bool autoApplySize;


    [HideInInspector] [SerializeField] Transform target;

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
    public void EncircleTarget()
    {
        transform.position = target.position;

    }
}
