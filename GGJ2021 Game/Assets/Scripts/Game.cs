using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField]
    private UIManager m_UIManager;
    [SerializeField]
    private PlayerManager m_PlayerManager;
    [SerializeField] 
    private DungeonItemSpawner dungeonItemSpawner;
    [SerializeField] 
    private NpcManager npcManager;
    [SerializeField]
    private Transform itemHolder;

    public PlayerManager PlayerManager => m_PlayerManager;

    public ItemGenerator ItemGenerator;

    private Item[] items;
    private IEnumerable<Item> dungeonItems;
    
    public UIManager UIManager => m_UIManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(this);
    }

    private void Start()
    {
        items = ItemGenerator.GenerateItems(itemHolder, 1000);
        dungeonItems = dungeonItemSpawner.SpawnItems(items);
        dungeonItemSpawner.SpawnEnemies();
        npcManager.pickLostItems(dungeonItems);
    }
}
