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

    void Start()
    {
        SetDisplayItem(null);
    }

    public void SetDisplayItem(Item toSet)
    {
        if (toSet == null)
        {
            gameObject.SetActive(false);
            _currentlyDisplayed = null;
        }
        else if (toSet != _currentlyDisplayed)
        {
            _currentlyDisplayed = toSet;

            gameObject.SetActive(true);

            NameText.text = toSet.Name;
            NameText.color = toSet.Rarity.NameColor;

            DescriptionText.text = toSet.GetItemInfo();

            ItemIcon.sprite = toSet.Icon;
        }
    }
}