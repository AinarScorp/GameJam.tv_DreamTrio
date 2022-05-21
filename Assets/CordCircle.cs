using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CordCircle : MonoBehaviour
{
    [SerializeField] float newRadius;
    [SerializeField] bool autoApplySize;

    [SerializeField] bool applySize;

    [SerializeField] bool applyNewPos;


    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        if (applyNewPos)
        {
            applyNewPos = false;
            transform.position = target.position;

        }
        if (autoApplySize)
        {

            transform.localScale = new Vector3(newRadius, newRadius);

        }
        else if (applySize)
        {
            applySize = false;
            transform.localScale = new Vector3(newRadius, newRadius);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " have entered");
    }
}
