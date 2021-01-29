using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory SpriteArrays")]
    public Image[] EquipmentSprites;
    public Image[] InventorySprites;
    public Image[] InventoryDropSprites;

    [Space, Header("Inventory ButtonArrays")]
    [SerializeField] private Button[] InventoryItemButtons;
    [SerializeField] private Button[] InventoryItemDropButtons;

    void Start()
    {
        Button btn;
        for (int i = 0; i < InventoryItemButtons.Length; i++)
        {
            btn = InventoryItemButtons[i].GetComponent<Button>();
            btn.onClick.AddListener(OnClickUseItem);
        }
        for (int i = 0; i < InventoryItemDropButtons.Length; i++)
        {
            btn = InventoryItemDropButtons[i].GetComponent<Button>();
            btn.onClick.AddListener(OnClickDropItem);
        }
    }

    public void OnClickUseItem()
    {
        Debug.Log("You have used the Iteam!");
    }

    public void OnClickDropItem()
    {
        Debug.Log("You have dropped the Iteam!");
    }
}
