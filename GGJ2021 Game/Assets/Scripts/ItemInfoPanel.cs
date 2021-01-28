using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelObject;
    [SerializeField]
    private TMP_Text NameText;
    [SerializeField]
    private TMP_Text DescriptionText;

    private Item DisplayedItem;

    // Start is called before the first frame update
    void Start()
    {
        CloseInfo();
    }

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

    

    private void CloseInfo()
    {
        PanelObject.SetActive(false);
        DisplayedItem = null;
    }

    public void DisplayInfo(Item _item)
    {
        DisplayedItem = _item;

        PanelObject.SetActive(true);

        NameText.text = _item.Name;
        NameText.color = _item.Rarity.NameColor;

        DescriptionText.text = _item.GetItemInfo();

        this.transform.position = _item.transform.position;
    }
}
