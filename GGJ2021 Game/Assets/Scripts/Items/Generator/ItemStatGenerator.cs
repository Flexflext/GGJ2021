using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class ItemStatGenerator
{
    public static int[] GenerateRandomStats(Item item)
    {
        var statDistribution = new int[Item.StatEnums.Length];

        DistributeStats(statDistribution, (int) item.Rarity.MaxStrength, true, 1);

        var kissCurse = Random.Range(0, 5);
        DistributeStats(statDistribution, kissCurse, false, -1);
        DistributeStats(statDistribution, kissCurse, false, 1);

        return statDistribution;
    }

    private static void DistributeStats(int[] arr, int points, bool withPrefer, int amount)
    {
        var prefer = Random.Range(0, arr.Length);

        for (var i = 0; i < points; i++)
        {
            if (withPrefer && Random.Range(0, 3) == 0)
            {
                arr[prefer] += amount;
            }
            else
            {
                arr[Random.Range(0, arr.Length)] += amount;
            }
        }
    }
}