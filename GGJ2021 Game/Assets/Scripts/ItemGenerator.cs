using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public enum ERarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legandary
    }

    [SerializeField]
    private ItemRarity[] ItemRarities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
