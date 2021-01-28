using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    private PlayerAttackScript playerAttack;
    private PlayerTopDownMovement playerMovement;
    private PlayerHealth playerHealth;



    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttackScript>();
        playerMovement = GetComponent<PlayerTopDownMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }


    private void ApplyBuffToPlayer(float _healthpercent, float _speedbuffpercent, float _attackbuffpercent, float _timetillpotionswearsoff)
    {
        StopCoroutine("TimeTillEffectsResets");

        playerAttack.DamageMultiplier *= _attackbuffpercent;
        playerMovement.MovementSpeedMultiplier *= _speedbuffpercent;

        if (_healthpercent * playerHealth.MaxHealth >= playerHealth.MaxHealth)
        {
            playerHealth.CurrentHealth = playerHealth.MaxHealth;
        }
        else if (_healthpercent * playerHealth.MaxHealth <= 0)
        {
            playerHealth.CurrentHealth = 1;
        }
        else
        {
            playerHealth.CurrentHealth += _healthpercent * playerHealth.MaxHealth;
        }

        StartCoroutine(TimeTillEffectsResets(_timetillpotionswearsoff));

    }

    IEnumerator TimeTillEffectsResets(float _time)
    {
        yield return new WaitForSeconds(_time);

        playerAttack.DamageMultiplier = 1;
        playerMovement.MovementSpeedMultiplier = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            PotionTestScript testPotion =  collision.gameObject.GetComponent<PotionTestScript>();
            ApplyBuffToPlayer(testPotion.healthPercent, testPotion.speedBuffPercent, testPotion.attackbuffPercent, testPotion.timeTillPotionsWearsOff);

            Destroy(collision.gameObject);
        }
    }
}
