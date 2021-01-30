using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        StartCoroutine(nameof(RegenerateHealth));
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int regen = Game.Instance.Player.GetComponent<PlayerStatScript>().GetStatValue(ItemStat.HealthRegen);
            CurrentHealth = Mathf.Clamp(CurrentHealth + regen, 1, MaxHealth);
        }
    }


    public void PlayerTakesDamge(float _damage)
    {
        CurrentHealth -= _damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}