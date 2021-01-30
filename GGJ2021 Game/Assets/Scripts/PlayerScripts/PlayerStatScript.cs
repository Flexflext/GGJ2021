using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatScript : MonoBehaviour
{
    private int[] _playerStats = new int[Item.StatEnums.Length];

    private readonly List<PlayerBuff> _activeBuffs = new List<PlayerBuff>();

    // private void Awake()
    // {
    //     RecalculateStats();
    // }

    public void RecalculateStats()
    {
        int[] playerStats = new int[Item.StatEnums.Length];

        var backpack = gameObject.GetComponent<Backpack>();
        SumStat(backpack.GetEquippedHead(), playerStats);
        SumStat(backpack.GetEquippedChest(), playerStats);
        SumStat(backpack.GetEquippedWeapon(), playerStats);

        foreach (PlayerBuff buff in _activeBuffs)
        {
            SumStat(buff.StatValues, playerStats);
        }

        _playerStats = playerStats;

        Game.Instance.UIManager.InventoryUI.PlayerStatText.SetText(StatValueVisualizer.ToString(playerStats));
    }

    private static void SumStat(EquipmentItem item, IList<int> playerStats)
    {
        if (item)
        {
            SumStat(item.StatValues, playerStats);
        }
    }
    
    private static void SumStat(IReadOnlyList<int> statValues, IList<int> playerStats)
    {
        for (int stat = 0; stat < statValues.Count; stat++)
        {
            playerStats[stat] += statValues[stat];
        }
    }

    public void Update()
    {
        for (var i = 0; i < _activeBuffs.Count; i++)
        {
            var activeBuff = _activeBuffs[i];
            var timeElapsed = (DateTime.Now - activeBuff.ActiveSince).TotalSeconds;

            if (timeElapsed > activeBuff.Duration)
            {
                _activeBuffs.RemoveAt(i);
                RecalculateStats();
            }
        }
    }

    public void ActivateBuff(PlayerBuff playerBuff)
    {
        playerBuff.ActiveSince = DateTime.Now;
        _activeBuffs.Add(playerBuff);

        RecalculateStats();
        var buffPanel = Game.Instance.UIManager.BuffPanel.GetComponent<BuffPanelScript>();
        buffPanel.addBuff(playerBuff);
    }

    public int GetStatValue(ItemStat stat)
    {
        return _playerStats[(int) stat];
    }
}