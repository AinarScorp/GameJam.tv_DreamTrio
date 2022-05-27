using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectToDrop 
{
    [SerializeField] GameObject dropObject;
    [SerializeField] [Range(0, 100)] float dropChancePercent;

    public GameObject DropObject { get => dropObject; }
    public float DropChancePercent { get => dropChancePercent; }
}
