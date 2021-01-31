using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcManager : MonoBehaviour
{
    private NPCWantedItem[] NpcWantedItems;

    private void Awake()
    {
        NpcWantedItems = GetComponentsInChildren<NPCWantedItem>();
    }

    public void pickLostItems(IEnumerable<Item> dungeonItems)
    {
        Item[] shuffled = dungeonItems.OrderBy(x => Random.Range(0F, 10F)).ToArray();
        for (int i = 0; i < NpcWantedItems.Length; i++)
        {
            NpcWantedItems[i].SetItem(shuffled[i]);
        }
    }
}
