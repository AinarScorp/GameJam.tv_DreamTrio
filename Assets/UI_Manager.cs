using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
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
        cordLengthText.text = $"Cord Length: {newAmount}";
    }
}
