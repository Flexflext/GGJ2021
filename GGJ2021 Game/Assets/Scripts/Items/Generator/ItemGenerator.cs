using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
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
        _itemRarities = Resources.LoadAll<ItemRarity>("StructuredObjects/ItemRarities").OrderBy(i => i.SpawnProbability);
        _itemTypes = Resources.LoadAll<ItemType>("StructuredObjects/ItemTypes").OrderBy(i => i.spawnProbability);
    }

    public IEnumerable<GameObject> GenerateItems(int amount)
    {
        List<GameObject> items = new List<GameObject>(amount);
        for (int i = 0; i < amount; i++)
        {
            var itemName = NameGenerator.generateName();
            var itemRarity = GenerateRarity();
            var itemType = GenerateItemType();
            var itemSprite = ItemSpriteGenerator.generateSprite(itemRarity, itemType);

            Instantiate(itemPrefab);
            var item = Instantiate(itemPrefab);
            item.name = $"{itemName} ({itemRarity.name}, {itemType.name})";
            
            var componentClass = Type.GetType(itemType.component);
            var itemComponent = (Item)item.AddComponent(componentClass);
            itemComponent.Name = itemName;
            itemComponent.Rarity = itemRarity;
            itemComponent.Icon = itemSprite;

            var spriteRenderer = item.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemSprite;
            //spriteRenderer.color = itemBehaviour.Rarity.NameColor;
            items.Add(item);
        }

        return items;
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
}