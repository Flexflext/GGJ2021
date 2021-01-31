using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Sound damageSound;
    [SerializeField] private Sound deathSound;

    private float _currentHealth;
    private float _maxHealth;

    Animator PlayerDamagedAnim;
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

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        PlayerDamagedAnim = GetComponent<Animator>();
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
        PlayerDamagedAnim.SetTrigger("IsDamaged");
        AudioManager.instance.PlaySound(damageSound);

        if (true) return;
        if (CurrentHealth <= 0)
        {
            AudioManager.instance.PlaySound(deathSound);
            
            Game.Instance.PlayerManager.Backpack.ClearBags();
            transform.position = startPos;
            AudioManager.instance.ChangeBackgroundMusic(AudioManager.EBackgroundMusicThemes.Hub);
            CurrentHealth = MaxHealth;
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