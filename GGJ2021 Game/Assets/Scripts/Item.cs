using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name;
    public ItemRarity Rarity;
    public Sprite Icon;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void GenerateRandomStats();
}
