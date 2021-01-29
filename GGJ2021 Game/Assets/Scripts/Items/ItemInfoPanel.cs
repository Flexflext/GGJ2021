using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelObject;
    [SerializeField]
    private TMP_Text NameText;
    [SerializeField]
    private TMP_Text DescriptionText;
    [SerializeField]
    private Image ItemIcon;

    private Item DisplayedItem;

    private List<Item> NearbyItemList;

    private Backpack PlayerBackpack;

    // Start is called before the first frame update
    void Start()
    {
        CloseInfo();
        NearbyItemList = new List<Item>();
        PlayerBackpack = GetComponent<Backpack>();
    }

    private void Update()
    {
        //ToDo check NearbyItemList

        if (Input.GetKeyDown(KeyCode.E) && NearbyItemList.Count > 0)
        {
            NearbyItemList[0].gameObject.SetActive(!PlayerBackpack.AddItem(NearbyItemList[0]));
            //NearbyItemList.RemoveAt(0);

            if (NearbyItemList.Count > 0)
            {
                DisplayInfo(NearbyItemList[0]);
            }
            else
            {
                CloseInfo();
            }
        }
    }

    /*
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit && DisplayedItem == null)
        {
            DisplayInfo(hit.transform.gameObject.GetComponent<Item>());
        }
        else if (!hit && PanelObject.activeSelf == true)
        {
            CloseInfo();
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ToDo add item to NearbyItemList


        if (collision.gameObject.CompareTag("Item") && DisplayedItem == null)
        {
            DisplayInfo(collision.gameObject.GetComponent<Item>());
            NearbyItemList.Add(collision.gameObject.GetComponent<Item>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //ToDo remove item from NearbyItemList
        NearbyItemList.Remove(collision.gameObject.GetComponent<Item>());
        CloseInfo();
    }

    public void CloseInfo()
    {
        PanelObject.SetActive(false);
        DisplayedItem = null;
    }

    public void DisplayInfo(Item _item)
    {
        if (_item)
        {
            DisplayedItem = _item;

            PanelObject.SetActive(true);

            NameText.text = _item.Name;
            NameText.color = _item.Rarity.NameColor;

            DescriptionText.text = _item.GetItemInfo();

            ItemIcon.sprite = _item.Icon;
            //this.transform.position = _item.transform.position;
        }
    }
}
