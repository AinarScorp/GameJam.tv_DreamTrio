using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnTestScript : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    [SerializeField] Material originalMaterial;
    JohnTestInput input;
    bool flashInProgress;

    private void Awake()
    {
        input = new JohnTestInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        input.Call.Click.performed += _ => CameraMethod();
        input.Call.Click2.performed += _ => EnemyFlash();
    }

    private void CameraMethod()
    {
        StartCoroutine(VirtualCamera.Instance.Shake(5, 2.2f, 0.2f));
    }

    private void EnemyFlash()
    {
        if (flashInProgress)
        {
            StopCoroutine(Flash());
        }

        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        flashInProgress = true;

        transform.GetComponent<SpriteRenderer>().material = flashMaterial;

        yield return new WaitForSeconds(0.1f);

        transform.GetComponent<SpriteRenderer>().material = originalMaterial;

        flashInProgress = false;
    }
}
