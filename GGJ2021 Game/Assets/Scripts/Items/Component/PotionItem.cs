using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PotionItem : Item, IUsable
{
    private PlayerBuff _itemBuff;

    public override void GenerateRandomStats()
    {
        var statValues = ItemStatGenerator.GenerateRandomStats(this);
        _itemBuff = new PlayerBuff
        {
            Icon = Icon,
            Duration = Random.Range(2,10),
            StatValues = statValues
        };
    }

    public void Use(Backpack backpack)
    {
        var playerStats = backpack.gameObject.GetComponent<PlayerStatScript>();
        playerStats.ActivateBuff(_itemBuff);
        backpack.DestroyItem(this);
    }

    public override string GetItemInfo()
    {
        return _itemBuff.GetBuffInfo();
    }
}