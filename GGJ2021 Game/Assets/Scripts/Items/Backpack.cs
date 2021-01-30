using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryUI.EquipmentSlot;

public class Backpack : MonoBehaviour
{
    public int Size;
    private Item[] Inventory;

    private HeadItem _equippedHead;
    private ChestItem _equippedChest;
    private WeaponItem _equippedWeapon;

    private List<Item> _nearbyItemList;

    void Start()
    {
        Inventory = new Item[Size];
        _nearbyItemList = new List<Item>();
    }

    private void Update()
    {
        var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
        if (_nearbyItemList.Count > 0)
        {
            Item nearest = _nearbyItemList[0];
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool success = AddItem(nearest);
                nearest.gameObject.SetActive(!success);
                _nearbyItemList.Remove(nearest);
            }
            else
            {
                itemInfoPanel.SetDisplayItem(nearest, false);
            }
        }
        else
        {
            itemInfoPanel.SetDisplayItem(null, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            _nearbyItemList.Add(collision.gameObject.GetComponent<Item>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _nearbyItemList.Remove(collision.gameObject.GetComponent<Item>());
    }

    private bool AddItem(Item _item)
    {
        for (var i = 0; i < Inventory.Length; i++)
        {
            if (!Inventory[i])
            {
                setItem(_item, i);
                return true;
            }
        }

        return false;
    }

    private Item setItem(Item _item, int i)
    {
        var previous = Inventory[i];
        Inventory[i] = _item;
        Game.Instance.UIManager.InventoryUI.SetItem(i, _item);
        return previous;
    }

    public void DropItem(int _slot)
    {
        Game.Instance.UIManager.InventoryUI.SetItem(_slot, null);

        Inventory[_slot].gameObject.SetActive(true);
        Inventory[_slot].gameObject.transform.position = transform.position;

        Inventory[_slot] = null;
    }

    public void UseItem(int _slot)
    {
        Item item = Inventory[_slot];
        if (item is IUsable usable)
        {
            usable.Use(this);
        }
        else if (item is HeadItem head)
        {
            setItem(_equippedHead, _slot);
            _equippedHead = head;

            Game.Instance.UIManager.InventoryUI.SetEquipped(Head, item);
            Game.Instance.Player.GetComponent<PlayerStatScript>().RecalculateStats();
        }
        else if (item is ChestItem chest)
        {
            setItem(_equippedChest, _slot);
            _equippedChest = chest;

            Game.Instance.UIManager.InventoryUI.SetEquipped(Chest, item);
            Game.Instance.Player.GetComponent<PlayerStatScript>().RecalculateStats();
        }
        else if (item is WeaponItem weapon)
        {
            setItem(_equippedWeapon, _slot);
            _equippedWeapon = weapon;

            Game.Instance.UIManager.InventoryUI.SetEquipped(Weapon, item);
            Game.Instance.Player.GetComponent<PlayerStatScript>().RecalculateStats();
        }
    }

    public void DestroyItem(Item _item)
    {
        int slot = GetInventorySlot(_item);

        if (slot != -1)
        {
            var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
            itemInfoPanel.SetDisplayItem(null, false);

            Game.Instance.UIManager.InventoryUI.SetItem(slot, null);

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

    public Item GetItem(int slotId)
    {
        return Inventory[slotId];
    }

    public WeaponItem GetEquippedWeapon()
    {
        return _equippedWeapon;
    }

    public ChestItem GetEquippedChest()
    {
        return _equippedChest;
    }

    public HeadItem GetEquippedHead()
    {
        return _equippedHead;
    }
}