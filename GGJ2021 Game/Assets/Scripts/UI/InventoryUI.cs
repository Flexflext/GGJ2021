using System;
using TMPro;
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
    
    [Space, Header("Misc")]
    public TMP_Text PlayerStatText;

    void Start()
    {
        Backpack backpack = Game.Instance.PlayerManager.Backpack;
        for (int i = 0; i < InventoryItemButtons.Length; i++)
        {
            SetItem(i, backpack.GetItem(i));

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
            SetEquipped(equipSlot, GetEquippedItemForSlot(backpack, equipSlot));

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
        if (item)
        {
            InventorySprites[slot].enabled = true;
            InventorySprites[slot].sprite = item.Icon;
            InventoryDropSprites[slot].enabled = true;
        }
        else
        {
            InventorySprites[slot].enabled = false;
            InventoryDropSprites[slot].enabled = false;
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