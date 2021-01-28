using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpriteGenerator
{
    private static Dictionary<string, Sprite[]> spriteCache = new Dictionary<string, Sprite[]>();

    public static Sprite generateSprite(ItemRarity rarity, ItemType itemType)
    {
        var path = $"ItemSprites/{itemType.name}/{rarity.name}";

        spriteCache.TryGetValue(path, out var sprites);
        if (sprites == null)
        {
            sprites = Resources.LoadAll(path, typeof(Sprite)).Cast<Sprite>().ToArray();
            spriteCache[path] = sprites;
        }

        return sprites[Random.Range(0, sprites.Length)];
    }
}