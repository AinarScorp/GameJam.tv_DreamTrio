using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectToDrop 
{
    public GameObject dropObject;
    [Range(0, 100)] public float dropChancePercent;



}
