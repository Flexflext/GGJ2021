using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DescriptionText;
    [SerializeField] private Image ItemIcon;

    private object _currentlyDisplayed;
    private bool mouseSelected = false;

    void Start()
    {
        SetDisplayItem(null, false);
    }

    public void SetDisplayItem(object toSet, bool mouse)
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
            mouseSelected = mouse;

            switch (toSet)
            {
                case Item item:
                    NameText.text = item.Name;
                    NameText.color = item.Rarity.NameColor;

                    DescriptionText.text = item.GetItemInfo();

                    ItemIcon.sprite = item.Icon;
                    break;
                case PlayerBuff buff:
                    NameText.text = null;
                    ItemIcon.sprite = buff.Icon;
                    DescriptionText.text = buff.GetBuffInfo();
                    break;
            }
        }
    }
}