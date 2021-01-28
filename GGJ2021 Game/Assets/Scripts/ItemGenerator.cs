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

    public enum EItemType
    {
        Unusable
    }

    private readonly Array _itemTypes = Enum.GetValues(typeof(EItemType));

    [SerializeField] private ItemRarity[] itemRarities;

    public List<GameObject> GenerateItems(int amount)
    {
        List<GameObject> items = new List<GameObject>(amount);
        for (int i = 0; i < amount; i++)
        {
            EItemType itemType = (EItemType) _itemTypes.GetValue(Random.Range(0, _itemTypes.Length));
            var itemName = NameGenerator.generateName();
            var itemRarity = itemRarities[(int) GenerateRarity()];
            var itemSprite = ItemSpriteGenerator.generateSprite(itemRarity, itemType);

            var item = new GameObject();
            item.name = itemName;

            var itemComponent = GenerateComponent(item, itemType);
            itemComponent.Name = itemName;
            itemComponent.Rarity = itemRarity;
            itemComponent.Icon = itemSprite;

            var spriteRenderer = item.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemSprite;
            //spriteRenderer.color = itemBehaviour.Rarity.NameColor;
            items.Add(item);
        }

        return items;
    }

    private static Item GenerateComponent(GameObject item, EItemType itemType)
    {
        switch (itemType)
        {
            case EItemType.Unusable:
                return item.AddComponent<UnusableItem>();
            default:
                throw new NotSupportedException(itemType + " not implemented");
        }
    }

    private ERarity GenerateRarity()
    {
        var rarity = Random.Range(0, 100);
        if (rarity < GetPropability(ERarity.Legendary))
        {
            return ERarity.Legendary;
        }

        if (rarity < GetPropability(ERarity.Epic))
        {
            return ERarity.Epic;
        }

        if (rarity < GetPropability(ERarity.Rare))
        {
            return ERarity.Rare;
        }

        if (rarity < GetPropability(ERarity.Uncommon))
        {
            return ERarity.Uncommon;
        }

        return ERarity.Common;
    }

    private int GetPropability(ERarity eRarity)
    {
        return itemRarities[(int) eRarity].SpawnProbability;
    }
}