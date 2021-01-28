using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemType", menuName = "ScriptableObjects/ItemType")]
public class ItemType : ScriptableObject
{
    // Between 0-100%
    public int spawnProbability;
    
    // Reference to the Item-Script that should be added to the created Item
    public string component; 
}