using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float CurrentSpeed;

    [SerializeField]
    float AttackRangeModifier;


    public GameObject EnemyDeathAnim;

    PlayerHealth Player;

    Animator EnemyAnim;

    bool EnemyHasCooldown;
    bool IsHittingWall;
    bool GotHit;


    Vector2 StartPos;

    Rigidbody2D Rb;


    [SerializeField]
    float KnockbackForce;


    [SerializeField] private Sound enemyHitSound;
    [SerializeField] private Sound enemyDeathSound;


    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerHealth>();
        EnemyAnim = GetComponentInChildren<Animator>();

        CurrentHealth = MaxHealth;
        CurrentDamage = MaxDamage;
        CurrentSpeed = MaxSpeed;
        //CurrentAttackSpeed = MaxAttackSpeed;
        CurrentAttackRange = MaxAttackRange;


        StartPos = transform.position;
    }

    private void Update()
    {
        if (EnemyHasCooldown == false)
        {
            StopCoroutine("EnemyAttackCooldown");
        }

    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    protected virtual void EnemyMove()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= CurrentAttackRange && IsHittingWall == false)
        {
            CurrentAttackRange += CurrentAttackRange * 2;
            Attack();
        }
    }


    protected virtual void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, CurrentSpeed * Time.deltaTime);


        //float speed = Vector2.SqrMagnitude(transform.position);

        //Rb.MovePosition(transform.position + Player.transform.position * CurrentSpeed * Time.deltaTime);

        EnemyAnim.SetFloat("Speed", CurrentSpeed);
        //EnemyAnim.SetFloat("Horizontal", (StartPos.x + transform.position.x));
        //EnemyAnim.SetFloat("Vertical", (StartPos.y + transform.position.y));



        if (Player.transform.position.x > transform.position.x)
        {
            Debug.Log("DDDD");
            EnemyAnim.SetFloat("Horizontal", 1);
            EnemyAnim.SetFloat("Vertical", 0);
        }
        if (Player.transform.position.x < transform.position.x)
        {
            Debug.Log("DDDD");

            EnemyAnim.SetFloat("Horizontal", -1);
            EnemyAnim.SetFloat("Vertical", 0);
        }
        if (Player.transform.position.y > transform.position.y)
        {
            Debug.Log("DDDD");

            EnemyAnim.SetFloat("Vertical", 1);
            EnemyAnim.SetFloat("Horizontal", 0);
        }
        if (Player.transform.position.y < transform.position.y)
        {
            EnemyAnim.SetFloat("Vertical", -1);
            EnemyAnim.SetFloat("Horizontal", 0);
        }


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
                CurrentSpeed -= CurrentSpeed;
                yield return new WaitForSeconds(1f);
                EnemyHasCooldown = false;
            }
            else
            {
                CurrentSpeed = MaxSpeed;
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
