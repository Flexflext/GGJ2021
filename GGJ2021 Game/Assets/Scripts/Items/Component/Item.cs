using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Item : MonoBehaviour
{
    public static Attribute[] StatEnums = Enum.GetValues(typeof(Attribute)).Cast<Attribute>().ToArray();

    public string Name;
    
    public Sprite Icon;
    public int GoldValue;
    public Light2D Light;

    private ItemRarity rarity;
    public ItemRarity Rarity
    {
        get { return rarity; }
        set
        {
            rarity = value;
            if (Light)
            {
                Light.color = rarity.NameColor;
            }
        }
    }

    public abstract string GetItemInfo();

    public abstract void GenerateRandomStats();
}