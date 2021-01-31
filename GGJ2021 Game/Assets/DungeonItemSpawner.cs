
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonItemSpawner : MonoBehaviour
{
    public IEnumerable<Item> SpawnItems(Item[] items)
    {
        List<Item> spawned = new List<Item>();
        IItemSpawnPoint[] spawnPoints = gameObject.GetComponentsInChildren<IItemSpawnPoint>();
        IItemSpawnPoint[] shuffledSpawnPoints = spawnPoints.OrderBy(x => Random.Range(0F, 10F)).ToArray();
        for (int index = 0; index < shuffledSpawnPoints.Length; index++)
        {
            if(index >= items.Length) break;
            items[index].transform.SetParent(shuffledSpawnPoints[index].transform, false);
            spawned.Add(items[index]);
        }

        return spawned;
    }
}
