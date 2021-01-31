using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Base_New : MonoBehaviour
{

    //[SerializeField]
    //float MaxAttackSpeed;
    //private float CurrentAttackSpeed;

    [SerializeField]
    float MaxAttackRange;
    private float CurrentAttackRange;

    [SerializeField]
    float MaxHealth;
    private float CurrentHealth;

    [SerializeField]
    float MaxDamage;
    private float CurrentDamage;


    [SerializeField]
    float MaxSpeed;


    [SerializeField]
    float AttackRangeModifier;

    public GameObject EnemyDeathAnim;

    PlayerHealth Player;

    Animator EnemyAnim;

    bool EnemyHasCooldown;

    Vector2 StartPos;

    Rigidbody2D Rb;


    [SerializeField]
    float KnockbackForce;

    [SerializeField]
    AIPath AStarPath;

    [SerializeField] private Sound enemyHitSound;
    [SerializeField] private Sound enemyDeathSound;

    void Start()
    {
        Player = FindObjectOfType<PlayerHealth>();
        EnemyAnim = GetComponent<Animator>();

        CurrentHealth = MaxHealth;
        CurrentDamage = MaxDamage;
        AStarPath.maxSpeed = MaxSpeed;
        //CurrentAttackSpeed = MaxAttackSpeed;
        //CurrentAttackRange = MaxAttackRange;


        StartPos = transform.position;
    }

    private void Update()
    {
        if (EnemyHasCooldown == false)
        {
            StopCoroutine("EnemyAttackCooldown");
        }

        if (Vector3.Distance(Player.transform.position, transform.position) <= CurrentAttackRange)
        {
            Attack();
            AStarPath.maxSpeed = MaxSpeed;
        }
        else if (Vector3.Distance(Player.transform.position, transform.position) > CurrentAttackRange)
            AStarPath.maxSpeed -= AStarPath.maxSpeed;
    }

    protected virtual void Attack()
    {


    }


    public void EnemyTakesDamage(float _damage)
    {
        CurrentHealth -= _damage;

        if (CurrentHealth <= 0)
        {
            AudioManager.instance.PlaySound(enemyDeathSound);
            Destroy(gameObject);

            GameObject deathAnim = Instantiate(EnemyDeathAnim, transform.position, transform.rotation);
        }
        AudioManager.instance.PlaySound(enemyHitSound);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHit = collision.collider.GetComponent<PlayerHealth>();


        if (collision.collider.GetComponent<PlayerHealth>())
        {
            playerHit.PlayerTakesDamge(CurrentDamage);
            EnemyHasCooldown = true;
            StartCoroutine("EnemyAttackCooldown");
        }
    }


    IEnumerator EnemyAttackCooldown()
    {
        while (true)
        {
            if (EnemyHasCooldown == true)
            {
                Debug.Log("Enemy has cooldown");
                AStarPath.maxSpeed -= AStarPath.maxSpeed;
                yield return new WaitForSeconds(1f);
                EnemyHasCooldown = false;
            }
            else
            {
                AStarPath.maxSpeed = MaxSpeed;
                yield return null;
            }
        }
    }
}
