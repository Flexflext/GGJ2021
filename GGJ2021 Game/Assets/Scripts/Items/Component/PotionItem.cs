using UnityEngine;

public class PotionItem : Item
{
    private PlayerBuff _itemBuff;
    
    public override void GenerateRandomStats()
    {
        var stats = DistributeStats(4, 10);
        _itemBuff = new PlayerBuff
        {
            HealOverTime = stats[0], 
            SpeedBuff = stats[1] * 0.3F, 
            AttackBuff = stats[2], 
            Duration = stats[3]
        };
    }

    private int[] DistributeStats(int amount, int points)
    {
        var arr = new int[amount];
        for (var i = 0; i < points; i++)
        {
            arr[Random.Range(0, amount)]++;
        }

        return arr;
    }

    public override void OnPickup(GameObject player)
    {
        var playerBuffs = player.GetComponent<PlayerBuffScript>();
        playerBuffs.Activate(_itemBuff);
    }
    
    public override string GetItemInfo()
    {
        return _itemBuff.GetBuffInfo();
    }
}
