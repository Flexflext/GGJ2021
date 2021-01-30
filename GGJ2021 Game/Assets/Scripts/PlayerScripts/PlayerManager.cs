using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Backpack Backpack { get; private set; }
    public PlayerStatScript PlayerStat { get; private set; }

    private void Awake()
    {
        Backpack = GetComponent<Backpack>();
        PlayerStat = GetComponent<PlayerStatScript>();
    }
}
