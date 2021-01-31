using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcManager : MonoBehaviour
{
    private QuestGiverScript[] QuestGiverScript;

    private void Awake()
    {
        QuestGiverScript = GetComponentsInChildren<QuestGiverScript>();
    }

    public void pickLostItems(List<Item> dungeonItems)
    {
        Item[] shuffled = dungeonItems.OrderBy(x => Random.Range(0F, 10F)).ToArray();
        for (int i = 0; i < QuestGiverScript.Length; i++)
        {
            if (!Game.Instance.PlayerManager.Backpack.FindItem(QuestGiverScript[i].WantedItem))
            {
                QuestGiverScript[i].SetItem(shuffled[i]);
                dungeonItems.Remove(QuestGiverScript[i].WantedItem);
            }
        }
    }
}
