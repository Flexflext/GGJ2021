using System.Collections.Generic;
using System.Text;

public class StatValueVisualizer
{
    private static Dictionary<ItemStat, string> statTranslations = new Dictionary<ItemStat, string>
    {
        {ItemStat.Health, "Health"},
        {ItemStat.HealthRegen, "HealthReg"},
        {ItemStat.Damage, "Damage"},
        {ItemStat.MovementSpeed, "Speed"},
        {ItemStat.AttackSpeed, "AttackSpeed"},
        {ItemStat.AttackRange, "Range"},
    };

    public static string ToString(int[] statValues)
    {
        var sb = new StringBuilder();
        foreach (ItemStat statEnum in Item.StatEnums)
        {
            int statValue = statValues[(int) statEnum];
            if (statValue != 0)
            {
                sb.Append($"{statTranslations[statEnum]}: {statValue}\n");
            }
        }

        return sb.ToString();
    }
}