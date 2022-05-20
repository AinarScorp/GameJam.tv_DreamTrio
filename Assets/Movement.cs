using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 movementInputs;

    PlayerInputs inputs;


    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void Start()
    {
        inputs.PlayerBasicStuff.Movement.performed += _ => movementInputs = _.ReadValue<Vector2>();
    }


}
