using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Base_New : MonoBehaviour
{
    [SerializeField]
    float MaxHealth;
    private float CurrentHealth;

    [SerializeField]
    float MaxDamage;
    private float CurrentDamage;

    [SerializeField]
    float MaxAttackSpeed;
    private float CurrentAttackSpeed;

    [SerializeField]
    float MaxSpeed;
    private float CurrentSpeed;

    [SerializeField]
    float MaxAttackRange;
    private float CurrentAttackRange;

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

    void Start()
    {
        Player = FindObjectOfType<PlayerHealth>();
        EnemyAnim = GetComponent<Animator>();

        CurrentHealth = MaxHealth;
        CurrentAttackSpeed = MaxAttackSpeed;
        CurrentDamage = MaxDamage;
        AStarPath.maxSpeed = MaxSpeed;
        CurrentAttackRange = MaxAttackRange;


        StartPos = transform.position;
    }

    private void Update()
    {
        if (EnemyHasCooldown == false)
        {
            StopCoroutine("EnemyAttackCooldown");
        }

        if (Input.GetMouseButtonDown(0))
        {
            CurrentHealth -= 20;
        }

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);

            GameObject deathAnim = Instantiate(EnemyDeathAnim, transform.position, transform.rotation);
        }
    }


    public void EnemyTakesDamage(float _damage)
    {
        CurrentHealth -= _damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);

            GameObject deathAnim = Instantiate(EnemyDeathAnim, transform.position, transform.rotation);
            AnimatorStateInfo deathAnimLenght = deathAnim.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (deathAnimLenght.length <= 0)
            {
                Destroy(deathAnim);
            }
        }
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


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CurrentAttackRange);
    }
}
