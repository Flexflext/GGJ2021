using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndyTestSceneController : MonoBehaviour
{
    [SerializeField]
    GameObject InventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        InventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle Inventory
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }
    }
}
