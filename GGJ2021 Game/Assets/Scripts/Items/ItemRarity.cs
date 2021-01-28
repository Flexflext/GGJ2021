using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemRarity", menuName = "ScriptableObjects/ItemRarity")]
public class ItemRarity : ScriptableObject
{
    // Between 0-100%
    public int SpawnProbability;
    
    //Color for the item name
    public Color NameColor;

    //Finders fee
    public int MaxGoldValue;

    //For attack and resistance value or other
    public float MaxStrength;

    //
    public int MaxDurability;

    //
    public int MaxUseDurability;
}