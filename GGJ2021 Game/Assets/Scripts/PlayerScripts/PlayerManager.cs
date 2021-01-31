using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Backpack Backpack { get; private set; }
    public PlayerStatScript PlayerStat { get; private set; }
    public PlayerHealth Health { get; private set; }
    
    public SpriteRenderer WeaponRenderer;

    private void Awake()
    {
        Backpack = GetComponent<Backpack>();
        PlayerStat = GetComponent<PlayerStatScript>();
        Health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        EquipmentItem weapon = Game.Instance.ItemGenerator.GenerateStarterWeapon(transform.parent);
        weapon.transform.position = transform.position - new Vector3(0, 1);
    }
}
