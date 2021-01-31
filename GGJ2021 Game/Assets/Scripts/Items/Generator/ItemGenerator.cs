using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject itemPrefab;

    private IEnumerable<ItemRarity> _itemRarities;
    private IEnumerable<ItemType> _itemTypes;

    private void Awake()
    {
        _itemRarities = Resources.LoadAll<ItemRarity>("StructuredObjects/ItemRarities")
            .OrderBy(i => i.SpawnProbability);
        _itemTypes = Resources.LoadAll<ItemType>("StructuredObjects/ItemTypes").OrderBy(i => i.spawnProbability);
    }

    public Item[] GenerateItems(Transform itemHolder, int amount)
    {
        Item[] items = new Item[amount];
        for (int i = 0; i < amount; i++)
        {
            var itemName = NameGenerator.generateName();
            var itemRarity = GenerateRarity();
            var itemType = GenerateItemType();
            var itemSprite = ItemSpriteGenerator.GenerateSprite(itemRarity, itemType);

            Item item = BuildItem(itemName, itemRarity, itemType, itemSprite, itemHolder);
            items[i] = item;
        }

        return items;
    }

    private Item BuildItem(string itemName, ItemRarity itemRarity, ItemType itemType, Sprite itemSprite, Transform itemHolder)
    {
        var item = Instantiate(itemPrefab, itemHolder);
        item.name = $"{itemName} ({itemRarity.name}, {itemType.name})";

        var componentClass = Type.GetType(itemType.component);
        var itemComponent = (Item) item.AddComponent(componentClass);
        itemComponent.Name = itemName;
        itemComponent.Light = item.GetComponentInChildren<Light2D>();
        itemComponent.Rarity = itemRarity;
        itemComponent.Icon = itemSprite;
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        //spriteRenderer.color = itemBehaviour.Rarity.NameColor;

        itemComponent.GenerateRandomStats();
        return itemComponent;
    }

    private ItemRarity GenerateRarity()
    {
        var rarity = Random.Range(0, 100);
        foreach (var itemRarity in _itemRarities)
        {
            if (itemRarity.SpawnProbability > rarity)
            {
                return itemRarity;
            }
        }

        throw new NotSupportedException(rarity + " not spawnable");
    }

    private ItemType GenerateItemType()
    {
        var rarity = Random.Range(0, 100);
        foreach (var itemType in _itemTypes)
        {
            if (itemType.spawnProbability > rarity)
            {
                return itemType;
            }
        }

        throw new NotSupportedException(rarity + " not spawnable");
    }

    public EquipmentItem GenerateStarterWeapon(Transform itemHolder)
    {
        var itemName = "Rusty Sword";
        var itemRarity = findRarity("Rarity_Common");
        var itemType = findType("Sword");
        var itemSprite = ItemSpriteGenerator.GenerateSprite(itemRarity, itemType);

        EquipmentItem item = (EquipmentItem) BuildItem(itemName, itemRarity, itemType, itemSprite, itemHolder);
        item.StatValues = new int[Item.StatEnums.Length];
        item.StatValues[(int) ItemStat.Damage] = 1;
        return item;
    }

    private ItemRarity findRarity(string name)
    {
        return _itemRarities.FirstOrDefault(itemRarity => itemRarity.name.Equals(name));
    }

    private ItemType findType(string name)
    {
        return _itemTypes.FirstOrDefault(itemRarity => itemRarity.name.Equals(name));
    }
}