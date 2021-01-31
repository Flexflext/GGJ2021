using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class NPCWantedItem : MonoBehaviour
{
    public Item item;

    private InfoScriptNPCCanvas canvasInfo;

    void Start()
    {
        canvasInfo = GetComponentInChildren<InfoScriptNPCCanvas>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        canvasInfo.NameText.text = item.Name;
        canvasInfo.ItemSprite.sprite = item.Icon;
        canvasInfo.NameText.color = item.Rarity.NameColor;
    }
}
