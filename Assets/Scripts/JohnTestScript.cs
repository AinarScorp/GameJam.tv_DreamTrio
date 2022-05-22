using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnTestScript : MonoBehaviour
{
    JohnTestInput input;

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

    }
}
