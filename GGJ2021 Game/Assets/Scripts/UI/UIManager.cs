using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject InfoPanel;
    public GameObject BuffPanel;
    public GameObject ItemInfoPanel;

    [FormerlySerializedAs("HeartUiScript")] [Space]
    public HeartUiManager HeartUiManager;
    public InventoryUI InventoryUI;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUI.gameObject.SetActive(!InventoryUI.gameObject.activeSelf);
        }
    }

}
