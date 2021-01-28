using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionItem : Item
{
    public float healthPercent = 1;
    public float speedBuffPercent = 2;
    public float attackBuffPercent = 2;
    public float timeTillPotionsWearsOff = 10;

    public override void GenerateRandomStats()
    {
        
    }

    public override string GetItemInfo()
    {
        return "";
    }

    public override void OnPickup(GameObject player)
    {
        var playerAttack = player.GetComponent<PlayerAttackScript>();
        var playerMovement = player.GetComponent<PlayerTopDownMovement>();
        var playerHealth = player.GetComponent<PlayerHealth>();

        StopCoroutine("TimeTillEffectsResets");

        playerAttack.DamageMultiplier *= attackBuffPercent;
        playerMovement.MovementSpeedMultiplier *= speedBuffPercent;

        if (healthPercent * playerHealth.MaxHealth >= playerHealth.MaxHealth)
        {
            playerHealth.CurrentHealth = playerHealth.MaxHealth;
        }
        else if (healthPercent * playerHealth.MaxHealth <= 0)
        {
            playerHealth.CurrentHealth = 1;
        }
        else
        {
            playerHealth.CurrentHealth += healthPercent * playerHealth.MaxHealth;
        }

        StartCoroutine(TimeTillEffectsResets(timeTillPotionsWearsOff, playerAttack, playerMovement));
    }

    private IEnumerator TimeTillEffectsResets(float time, PlayerAttackScript playerAttack, PlayerTopDownMovement playerMovement)
    {
        yield return new WaitForSeconds(time);

        playerAttack.DamageMultiplier = 1;
        playerMovement.MovementSpeedMultiplier = 1;
    }
}