using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DescriptionText;
    [SerializeField] private Image ItemIcon;

    private Item _currentlyDisplayed;
    private bool mouseSelected = false;

    void Start()
    {
        SetDisplayItem(null, false);
    }

    public void SetDisplayItem(Item toSet, bool mouse)
    {
        if (mouseSelected && !mouse)
        {
            return;
        }

        if (toSet == null)
        {
            gameObject.SetActive(false);
            _currentlyDisplayed = null;
            mouseSelected = false;
        }
        else if (toSet != _currentlyDisplayed)
        {
            _currentlyDisplayed = toSet;

            gameObject.SetActive(true);

            NameText.text = toSet.Name;
            NameText.color = toSet.Rarity.NameColor;

            DescriptionText.text = toSet.GetItemInfo();

            ItemIcon.sprite = toSet.Icon;

            mouseSelected = mouse;
        }
    }
}