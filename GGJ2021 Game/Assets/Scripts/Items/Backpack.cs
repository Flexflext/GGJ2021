using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public int Size;
    private Item[] Inventory;
    private List<Item> NearbyItemList;

    void Start()
    {
        Inventory = new Item[Size];
        NearbyItemList = new List<Item>();
    }

    private void Update()
    {
        var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
        if (NearbyItemList.Count > 0)
        {
            Item nearest = NearbyItemList[0];
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool success = AddItem(nearest);
                nearest.gameObject.SetActive(!success);
                NearbyItemList.Remove(nearest);
            }
            else
            {
                itemInfoPanel.SetDisplayItem(nearest);
            }
        }
        else
        {
            itemInfoPanel.SetDisplayItem(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            NearbyItemList.Add(collision.gameObject.GetComponent<Item>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NearbyItemList.Remove(collision.gameObject.GetComponent<Item>());
    }

    public bool AddItem(Item _item)
    {
        for (var i = 0; i < Inventory.Length; i++)
        {
            if (!Inventory[i])
            {
                Inventory[i] = _item;
                
                Game.Instance.UIManager.InventoryUI.SetItem(i, _item);
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(int _slot)
    {
        Game.Instance.UIManager.InventoryUI.RemoveItem(_slot);

        Inventory[_slot].gameObject.SetActive(true);
        Inventory[_slot].gameObject.transform.position = transform.position;

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
            var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
            itemInfoPanel.SetDisplayItem(null);

            Game.Instance.UIManager.InventoryUI.RemoveItem(slot);

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