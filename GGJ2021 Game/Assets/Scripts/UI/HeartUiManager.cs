using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUiManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHearthSprite;
    [SerializeField] private Sprite emptyHearthSprite;

    public void OnHealthChange(float current, float max)
    {
        for (int x = transform.childCount - 1; x >= max; x--)
        {
            Destroy(transform.GetChild(x).gameObject);
        }

        while (transform.childCount - max < 0)
        {
            Instantiate(heartPrefab, transform);
        }

        
        for (int i = 0; i < max; i++)
        {
            Image image = transform.GetChild(i).GetComponent<Image>();
            image.sprite = current >= i + 1 ? fullHearthSprite : emptyHearthSprite;
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        int maxPerRow = 20;
        rectTransform.sizeDelta = new Vector2(32 + 64 * Math.Min(max, maxPerRow),
            ((max - 1) / maxPerRow + 1) * 96);
    }
}