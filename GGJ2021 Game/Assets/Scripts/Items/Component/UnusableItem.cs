using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnusableItem : Item
{
    public override string GetItemInfo()
    {
        return "A lost object waiting to be found by its owner";
    }

    public override void GenerateRandomStats()
    {
        GoldValue = Random.Range(0, Rarity.MaxGoldValue + 1);
    }
}
