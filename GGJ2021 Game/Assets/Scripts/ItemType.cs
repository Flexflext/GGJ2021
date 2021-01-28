using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemType", menuName = "ScriptableObjects/ItemType")]
public class ItemType : ScriptableObject
{
    // Between 0-100%
    public int spawnProbability;
    public string component; 
}