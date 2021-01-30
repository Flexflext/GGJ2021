using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Sound damageSound;

    private float _currentHealth;
    private float _maxHealth;

    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            if (_maxHealth != value)
            {
                _maxHealth = value;
                _currentHealth = Math.Max(_maxHealth, _currentHealth);
                Game.Instance.UIManager.HeartUiManager.OnHealthChange(_currentHealth, _maxHealth);
            }
        }
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;
                Game.Instance.UIManager.HeartUiManager.OnHealthChange(_currentHealth, _maxHealth);
            }
        }
    }

    private void Start()
    {
        MaxHealth = 3;
        CurrentHealth = MaxHealth;
        StartCoroutine(nameof(RegenerateHealth));
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float regen = 0.3F + Game.Instance.PlayerManager.PlayerStat.GetStatValue(ItemStat.HealthRegen);
            CurrentHealth = Mathf.Clamp(CurrentHealth + regen, 1, MaxHealth);
        }
    }


    public void PlayerTakesDamge(float _damage)
    {
        CurrentHealth -= _damage;

        AudioManager.instance.PlaySound(damageSound);
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     MaxHealth += 1;
        // }
    }

    public void OnStatUpdated(PlayerStatScript statScript)
    {
        MaxHealth = 3 + statScript.GetStatValue(ItemStat.Health);
    }
}