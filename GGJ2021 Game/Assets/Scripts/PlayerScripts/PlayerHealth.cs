using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    Animator PlayerDamagedAnim;


    private void Start()
    {
        PlayerDamagedAnim = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }


    public void PlayerTakesDamge(float _damage)
    {
        CurrentHealth -= _damage;
        PlayerDamagedAnim.SetTrigger("IsDamaged");

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
