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
    public DungeonItemSpawner dungeonItemSpawner;
    [SerializeField]
    public NpcManager npcManager;
    [SerializeField]
    public Transform itemHolder;
    [HideInInspector] public DungeonGenerator DungeonGenerator;

    public PlayerManager PlayerManager => m_PlayerManager;

    public ItemGenerator ItemGenerator;

    
    public List<Item> dungeonItems;

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
        DungeonGenerator = GetComponentInChildren<DungeonGenerator>();
        //dungeonItemSpawner.SpawnEnemies();
    }


}
