using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonItemSpawner : MonoBehaviour
{
    public IEnumerable<Item> SpawnItems(IEnumerable<Item> items)
    {
        //gameObject.GetComponentsInChildren<>();
        return items;
    }
}
