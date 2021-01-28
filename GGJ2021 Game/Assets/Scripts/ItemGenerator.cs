using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ItemGenerator : MonoBehaviour
{
    private enum ERarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [SerializeField] private Sprite itemSprite;
    [SerializeField] private ItemRarity[] itemRarities;

    public List<GameObject> GenerateItems(int amount)
    {
        List<GameObject> items = new List<GameObject>(amount);
        for (int i = 0; i < amount; i++)
        {
            var item = new GameObject();
            var itemBehaviour = GenerateBehaviour(item);
            itemBehaviour.Name = NameGenerator.generateName();
            itemBehaviour.Rarity = itemRarities[(int) GenerateRarity()];
            item.name = itemBehaviour.Name;
            itemBehaviour.Icon = itemSprite;
            
            var spriteRenderer = item.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemSprite;
            spriteRenderer.color = itemBehaviour.Rarity.NameColor;
            items.Add(item);
        }

        return items;
    }

    private static Item GenerateBehaviour(GameObject item)
    {
        //TODO more item types
        return item.AddComponent<UnusableItem>();
    }

    private static ERarity GenerateRarity()
    {
        var rarity = Random.Range(0, 100);
        if (rarity >= 99)
        {
            return ERarity.Legendary;
        }

        if (rarity >= 97)
        {
            return ERarity.Epic;
        }

        if (rarity >= 94)
        {
            return ERarity.Rare;
        }

        if (rarity >= 70)
        {
            return ERarity.Uncommon;
        }

        return ERarity.Common;
    }
}