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
        var generalPath = $"ItemSprites/{itemType.name}/{rarity.name}";

        spriteCache.TryGetValue(path, out var sprites);
        if (sprites == null)
        {
            // There are some sprites that apply for every rarity-level.
            // We union the rarity-specific and general sprites the rarity-sprites are not loaded yet
            var raritySprites = GetOrLoadSprites(path);
            var generalSprites = GetOrLoadSprites($"ItemSprites/{itemType.name}");
            sprites = raritySprites.Union(generalSprites).ToArray();
            spriteCache[path] = sprites;
        }
        
        return sprites[Random.Range(0, sprites.Length)];
    }

    private static Sprite[] GetOrLoadSprites(string path)
    {
        spriteCache.TryGetValue(path, out var sprites);
        if (sprites == null)
        {
            sprites = Resources.LoadAll(path, typeof(Sprite)).Cast<Sprite>().ToArray();
            spriteCache[path] = sprites;
        }

        return sprites;
    }
}