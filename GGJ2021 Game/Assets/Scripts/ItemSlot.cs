using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemInfoPanel InfoPanel;
    public Item StoredItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (StoredItem)
        {
            InfoPanel.DisplayInfo(StoredItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (StoredItem)
        {
            InfoPanel.CloseInfo();
        }
    }
}
