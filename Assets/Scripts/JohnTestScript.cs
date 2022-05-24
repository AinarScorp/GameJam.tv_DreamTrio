using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnTestScript : MonoBehaviour
{
    [SerializeField] FlashEffect flashScript;
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
        input.Call.Click2.performed += _ => flashScript.StartFlash();
    }

    private void CameraMethod()
    {
    }

    private void Flash()
    {

    }
}
