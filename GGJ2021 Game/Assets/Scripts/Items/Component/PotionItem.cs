using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PotionItem : Item, IUsable
{
    private PlayerBuff _itemBuff;

    public override void GenerateRandomStats()
    {
        var stats = new int[4];
        DistributeStats(stats, (int) Rarity.MaxStrength, true, 1);

        var kissCurse = Random.Range(0, 5);
        DistributeStats(stats, kissCurse, false, -1);
        DistributeStats(stats, kissCurse, false, 1);

        _itemBuff = new PlayerBuff
        {
            Icon = Icon,
            HealOverTime = stats[0],
            SpeedBuff = stats[1] * 0.3F,
            AttackBuff = stats[2],
            Duration = stats[3]
        };
    }

    private void DistributeStats(int[] arr, int points, bool withPrefer, int amount)
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

    public void Use(Backpack backpack)
    {
        var playerBuffs = backpack.gameObject.GetComponent<PlayerBuffScript>();
        playerBuffs.Activate(_itemBuff);
        //backpack.Destroy(this);
    }

    public override void OnPickup(GameObject player)
    {
    }

    public override string GetItemInfo()
    {
        return _itemBuff.GetBuffInfo();
    }
}