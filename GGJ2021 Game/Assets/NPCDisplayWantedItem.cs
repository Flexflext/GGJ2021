using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class NPCDisplayWantedItem : MonoBehaviour
{
    public Sprite ItemImage;
    public Color RarityColor;
    public string ItemName;

    private InfoScriptNPCCanvas canvasInfo;

    // Start is called before the first frame update
    void Start()
    {
        canvasInfo = GetComponentInChildren<InfoScriptNPCCanvas>();

        canvasInfo.NameText.text = ItemName;
        canvasInfo.ItemSprite.sprite = ItemImage;
        canvasInfo.NameText.color = RarityColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
