using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField] bool isHearth;
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(this.name);

        if (isHearth)
        {
            FindObjectOfType<PlayerHealth>().AddCollectedHearth();
            this.gameObject.SetActive(false);
        }
    }
}
