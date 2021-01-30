using System.Collections.Generic;
using System.Text;

public class StatValueVisualizer
{
    // private static Dictionary<ItemStats, string> statTranslations = new Dictionary<ItemStats, string>
    // {
    //     {ItemStats.Damage, "Damage"},
    //     {ItemStats.Health, "Damage"},
    //     {ItemStats.AttackRange, "Damage"},
    //     {ItemStats.HealthRegen, "Damage"},
    //     {ItemStats.Damage, "Damage"},
    //     {ItemStats.Damage, "Damage"},
    //     {ItemStats.Damage, "Damage"},
    // };

    public static string ToString(int[] statValues)
    {
        var sb = new StringBuilder();
        foreach (ItemStat statEnum in Item.StatEnums)
        {
            int statValue = statValues[(int) statEnum];
            if (statValue != 0)
            {
                sb.Append($"{statEnum.ToString()}: {statValue}\n");
            }
        }

        return sb.ToString();
    }
}