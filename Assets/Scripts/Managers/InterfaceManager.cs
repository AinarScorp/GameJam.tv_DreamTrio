using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] CordCircle cordCircle;
    [SerializeField] TextMeshProUGUI cordLengthText;


    private void Awake()
    {
        if (cordCircle == null)
            cordCircle = FindObjectOfType<CordCircle>();
    }

    public void DisplayNewCordLength(float newAmount)
    {
        cordLengthText.text = $"Cord Length: {Mathf.Round(newAmount *10)*0.1f}";
    }
}
