using System;
using System.Text;
using UnityEngine;

public class PlayerBuff
{
    // Buff duration
    public float Duration = 0F;

    // Each second the player get's healed by this amount
    public float HealOverTime = 0F;

    // The players speed gets multiplied by that amount
    public float SpeedBuff = 0F;

    // The players attack gets multiplied by that amount
    public float AttackBuff = 0F;

    // When was the buff activated
    public DateTime ActiveSince;

    // How often did the buff tick (relevant for hots)
    public int Ticked = -1;
    
    public Sprite Icon = null;

    public string GetBuffInfo()
    {
        var sb = new StringBuilder();
        if (HealOverTime != 0)
        {
            sb.Append($"Heal over time {HealOverTime}\n");
        }

        if (SpeedBuff != 0)
        {
            sb.Append($"Speed Buff {SpeedBuff}\n");
        }
        
        if (AttackBuff != 0)
        {
            sb.Append($"Attack Buff {AttackBuff}\n");
        }

        return sb.ToString();
    }
}