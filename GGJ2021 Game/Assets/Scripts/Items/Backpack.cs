using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryUI.EquipmentSlot;

public class Backpack : MonoBehaviour
{
    public int Size;
    private Item[] Inventory;

    private HeadItem EquippedHead;
    private ChestItem EquippedChest;
    private WeaponItem EquippedWeapon;

    private List<Item> NearbyItemList;

    [SerializeField] private Sound ConsumePotSound;
    [SerializeField] private Sound EquipItemSound;
    [SerializeField] private Sound PickUpItemSound;
    [SerializeField] private Sound DropItemSound;
    
    private int _money = 0;
    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            Game.Instance.UIManager.InventoryUI.MoneyText.text = $"{_money}";
        }
    }

    void Start()
    {
        Inventory = new Item[Size];
        NearbyItemList = new List<Item>();
        Money = 0;
    }

    private void Update()
    {
        var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
        if (NearbyItemList.Count > 0)
        {
            Item nearest = NearbyItemList[0];
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupItem(nearest);
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
            NearbyItemList.Add(collision.gameObject.GetComponent<Item>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NearbyItemList.Remove(collision.gameObject.GetComponent<Item>());
    }

    private int PickupItem(Item _item)
    {
        for (var i = 0; i < Inventory.Length; i++)
        {
            if (!Inventory[i])
            {
                _item.gameObject.SetActive(false);
                NearbyItemList.Remove(_item);
                SetItem(_item, i);
                AudioManager.instance.PlaySound(PickUpItemSound);
                return i;
            }
        }

        return -1;
    }

    private Item SetItem(Item _item, int i)
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

        AudioManager.instance.PlaySound(DropItemSound);

        Inventory[_slot] = null;
    }

    public void UseItem(int _slot)
    {
        Item item = Inventory[_slot];
        if (item is IUsable usable)
        {
            AudioManager.instance.PlaySound(ConsumePotSound);
            usable.Use(this);
        }
        else if (item is HeadItem head)
        {
            SetItem(EquippedHead, _slot);
            EquippedHead = head;

            Game.Instance.UIManager.InventoryUI.SetEquipped(Head, item);
            Game.Instance.PlayerManager.PlayerStat.RecalculateStats();
            AudioManager.instance.PlaySound(EquipItemSound);
        }
        else if (item is ChestItem chest)
        {
            SetItem(EquippedChest, _slot);
            EquippedChest = chest;

            Game.Instance.UIManager.InventoryUI.SetEquipped(Chest, item);
            Game.Instance.PlayerManager.PlayerStat.RecalculateStats();
            AudioManager.instance.PlaySound(EquipItemSound);
        }
        else if (item is WeaponItem weapon)
        {
            SetItem(EquippedWeapon, _slot);
            EquippedWeapon = weapon;

            Game.Instance.UIManager.InventoryUI.SetEquipped(Weapon, item);
            Game.Instance.PlayerManager.PlayerStat.RecalculateStats();
            Game.Instance.PlayerManager.WeaponRenderer.sprite = weapon.Icon;
            Game.Instance.PlayerManager.WeaponRenderer.enabled = true;
            AudioManager.instance.PlaySound(EquipItemSound);
        }
    }

    public bool DestroyItem(Item _item)
    {
        if (!_item) return false;
        
        int slot = GetInventorySlot(_item);

        if (slot != -1)
        {
            var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
            itemInfoPanel.SetDisplayItem(null, false);

            Game.Instance.UIManager.InventoryUI.SetItem(slot, null);

            Inventory[slot] = null;
            Destroy(_item.gameObject);
            return true;
        }

        if (EquippedWeapon == _item)
        {
            EquippedWeapon = null;
            onEquipChanged(Weapon);
            return true;
        }
        else if (EquippedChest == _item)
        {
            EquippedChest = null;
            onEquipChanged(Chest);
            return true;
        }
        else if (EquippedHead == _item)
        {
            EquippedHead = null;
            onEquipChanged(Head);
            return true;
        }

        return false;
    }

    private static void onEquipChanged(InventoryUI.EquipmentSlot equipmentSlot)
    {
        Game.Instance.UIManager.InventoryUI.SetEquipped(equipmentSlot, null);
        Game.Instance.PlayerManager.PlayerStat.RecalculateStats();
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
        return EquippedWeapon;
    }

    public ChestItem GetEquippedChest()
    {
        return EquippedChest;
    }

    public HeadItem GetEquippedHead()
    {
        return EquippedHead;
    }

    public void ClearBags()
    {
        foreach (Item item in Inventory)
        {
            DestroyItem(item);
        }

        DestroyItem(EquippedHead);
        DestroyItem(EquippedChest);
        DestroyItem(EquippedWeapon);

        Money = 0;
    }
}