using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Item : MonoBehaviour
{
    public string Name;

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

    public Sprite Icon;
    public int GoldValue;
    public Light2D Light;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomStats();
    }

    public abstract string GetItemInfo();

    public abstract void GenerateRandomStats();

    public virtual void OnPickup(GameObject player)
    {
    }
}