using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name;
    public ItemRarity Rarity;
    public Sprite Icon;
    public int GoldValue;

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
