using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectToDrop 
{
    [SerializeField] GameObject dropObject;
    [Range(0, 100)] public float dropChancePercent;

    public GameObject DropObject { get => dropObject; }
}
