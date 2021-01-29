﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public int Size;
    private Item[] Inventory;
    public Sprite Icon;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = new Item[Size];
    }

    public bool AddItem(Item _item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (!Inventory[i])
            {
                Inventory[i] = _item;
                Game.Instance.UIManager.InventoryUI.InventorySprites[i].enabled = true;
                Game.Instance.UIManager.InventoryUI.InventoryDropSprites[i].enabled = true;
                Game.Instance.UIManager.InventoryUI.InventorySprites[i].sprite = _item.Icon;
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(int _slot)
    {
        Game.Instance.UIManager.InventoryUI.InventorySprites[_slot].enabled = false;
        Game.Instance.UIManager.InventoryUI.InventoryDropSprites[_slot].enabled = false;

        Inventory[_slot].gameObject.SetActive(true);
        Inventory[_slot].gameObject.transform.position = this.transform.position;

        Inventory[_slot] = null;
    }

    public void UseItem(int _slot)
    {
        if (Inventory[_slot] is IUsable usable)
        {
            usable.Use(this);
        }
    }

    public void EquipItem(Item _item)
    {
        int slot = GetInventorySlot(_item);

        if (slot != -1)
        {
            
        }
    }

    public void DestroyItem(Item _item)
    {
        int slot = GetInventorySlot(_item);

        if (slot != -1)
        {
            Inventory[slot] = null;
            Destroy(_item.gameObject);
        }
    }

    private int GetInventorySlot(Item _item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == _item)
            {
                return i;
            }
        }

        return -1;
    }
}
