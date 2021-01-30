
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public abstract class EquipmentItem : Item
{
    public int[] StatValues;

    public override void GenerateRandomStats()
    {
        StatValues = ItemStatGenerator.GenerateRandomStats(this);
    }

    public override string GetItemInfo()
    {
       return StatValueVisualizer.ToString(StatValues);
    }
}