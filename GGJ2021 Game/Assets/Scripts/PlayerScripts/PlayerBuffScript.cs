using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerBuffScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerTopDownMovement playerMovement;
    private PlayerAttackScript playerAttack;

    public List<PlayerBuff> activeBuffs = new List<PlayerBuff>();

    private void Awake()
    {
        playerMovement = GetComponent<PlayerTopDownMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttackScript>();
    }

    void Update()
    {
        for (var i = 0; i < activeBuffs.Count; i++)
        {
            var activeBuff = activeBuffs[i];
            var timeElapsed = (DateTime.Now - activeBuff.ActiveSince).TotalSeconds;
            var ticks = (int) timeElapsed;
            if (activeBuff.Ticked != ticks)
            {
                activeBuff.Ticked = ticks;
                playerHealth.CurrentHealth = Mathf.Clamp(
                    playerHealth.CurrentHealth + activeBuff.HealOverTime,
                    1, playerHealth.MaxHealth
                );
            }

            if (timeElapsed > activeBuff.Duration)
            {
                activeBuffs.RemoveAt(i);
                OnRemove(activeBuff);
            }
        }
    }

    public void Activate(PlayerBuff playerBuff)
    {
        playerBuff.ActiveSince = DateTime.Now;
        activeBuffs.Add(playerBuff);

        playerAttack.DamageMultiplier += playerBuff.AttackBuff;
        playerMovement.MovementSpeedMultiplier += playerBuff.SpeedBuff;
    }

    private void OnRemove(PlayerBuff toRemove)
    {
        playerAttack.DamageMultiplier -= toRemove.AttackBuff;
        playerMovement.MovementSpeedMultiplier -= toRemove.SpeedBuff;
    }
}