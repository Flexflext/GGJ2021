﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory SpriteArrays")] public Image[] EquipmentSprites;
    public Image[] InventorySprites;
    public Image[] InventoryDropSprites;

    [Space, Header("Inventory ButtonArrays")] [SerializeField]
    private Button[] InventoryItemButtons;

    [SerializeField] private Button[] InventoryItemDropButtons;
    [SerializeField] private GameObject inventoryUi;

    void Start()
    {
        Backpack backpack = Game.Instance.Player.GetComponent<Backpack>();
        for (int i = 0; i < InventoryItemButtons.Length; i++)
        {
            SetItem(i, null);

            var slotId = i;
            Button itemButton = InventoryItemButtons[i];
            itemButton.onClick.AddListener(() =>
            {
                Debug.Log("Use Item " + slotId);
                backpack.UseItem(slotId);
            });

            Button itemDropButton = InventoryItemDropButtons[i];
            itemDropButton.onClick.AddListener(() =>
            {
                Debug.Log("Drop Item " + slotId);
                backpack.DropItem(slotId);
            });

            OnPointer onPointer = itemButton.gameObject.AddComponent<OnPointer>();
            onPointer.AddEnterListener(e =>
            {
                var item = backpack.GetItem(slotId);
                var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
                itemInfoPanel.SetDisplayItem(item, true);
            });
            onPointer.AddExitListener(e =>
            {
                var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
                itemInfoPanel.SetDisplayItem(null, true);
            });
        }

        for (var i = 0; i < EquipmentSprites.Length; i++)
        {
            EquipmentSlot equipSlot = (EquipmentSlot) i;
            SetEquipped(equipSlot, null);

            OnPointer onPointer = EquipmentSprites[i].gameObject.AddComponent<OnPointer>();
            onPointer.AddEnterListener(e =>
            {
                var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
                itemInfoPanel.SetDisplayItem(GetEquippedItemForSlot(backpack, equipSlot), true);
            });
            onPointer.AddExitListener(e =>
            {
                var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
                itemInfoPanel.SetDisplayItem(null, true);
            });
        }
    }

    private static EquipmentItem GetEquippedItemForSlot(Backpack backpack, EquipmentSlot equipSlot)
    {
        switch (equipSlot)
        {
            case EquipmentSlot.Head:
                return backpack.GetEquippedHead();
            case EquipmentSlot.Chest:
                return backpack.GetEquippedChest();
            case EquipmentSlot.Weapon:
                return backpack.GetEquippedWeapon();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetItem(int slot, Item item)
    {
        if (item == null)
        {
            InventorySprites[slot].enabled = false;
            InventoryDropSprites[slot].enabled = false;
        }
        else
        {
            InventorySprites[slot].enabled = true;
            InventoryDropSprites[slot].enabled = true;
            InventorySprites[slot].sprite = item.Icon;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUi.SetActive(!inventoryUi.activeSelf);
        }
    }

    public enum EquipmentSlot
    {
        Head,
        Chest,
        Weapon
    }

    public void SetEquipped(EquipmentSlot slot, Item item)
    {
        if (item == null)
        {
            EquipmentSprites[(int) slot].enabled = false;
        }
        else
        {
            EquipmentSprites[(int) slot].enabled = true;
            EquipmentSprites[(int) slot].sprite = item.Icon;
        }
    }
}