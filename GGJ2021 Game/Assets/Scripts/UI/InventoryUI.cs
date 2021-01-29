using System;
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

    [SerializeField] private GameObject inventoryUi;
    
    void Start()
    {
        Backpack backpack = Game.Instance.Player.GetComponent<Backpack>();
        for (int i = 0; i < InventoryItemButtons.Length; i++)
        {
            RemoveItem(i);
            
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
                //Debug.Log("Mouse entered " + slotId);
                var item = backpack.GetItem(slotId);
                var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
                itemInfoPanel.SetDisplayItem(item, true);
            });
            onPointer.AddExitListener(e =>
            {
                //Debug.Log("Mouse exit " + slotId);
                var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
                itemInfoPanel.SetDisplayItem(null, true);
            });

        }
    }

    public void SetItem(int slot, Item item)
    {
        InventorySprites[slot].enabled = true;
        InventoryDropSprites[slot].enabled = true;
        InventorySprites[slot].sprite = item.Icon;
    }

    public void RemoveItem(int slot)
    {
        InventorySprites[slot].enabled = false;
        InventoryDropSprites[slot].enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUi.SetActive(!inventoryUi.activeSelf);
        }
    }
}