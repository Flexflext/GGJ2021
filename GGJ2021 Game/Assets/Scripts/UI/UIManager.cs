using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Panels")]
    public GameObject InventoryPanel;
    public GameObject InfoPanel;

    [Space,SerializeField]
    private InventoryUI InventoryUI;

    public void SwitchInventoryActive()
    {
        if (InventoryPanel.activeSelf)
        {
            InventoryPanel.SetActive(false);
        }
        else
        {
            InventoryPanel.SetActive(true);
        }
    }
}
