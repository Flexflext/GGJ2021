using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff
{
    public int[] StatValues;
 
    // Buff duration
    public float Duration = 0F;

    // When was the buff activated
    public DateTime ActiveSince;

    public Sprite Icon = null;

    public string GetBuffInfo()
    {
        return StatValueVisualizer.ToString(StatValues);
    }
}