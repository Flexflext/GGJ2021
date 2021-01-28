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
