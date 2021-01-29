using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BASE : MonoBehaviour
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

    bool GotHit;
    bool IsHittingWall;

    [SerializeField]
    float KnockbackForce;


    void Start()
    {
        EnemyDeathAnim = GetComponent<GameObject>();
        Player = FindObjectOfType<PlayerHealth>();
        EnemyAnim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();

        CurrentHealth = MaxHealth;
        CurrentAttackSpeed = MaxAttackSpeed;
        CurrentDamage = MaxDamage;
        CurrentSpeed = MaxSpeed;
        CurrentAttackRange = MaxAttackRange;

        StartPos = transform.position;
    }

    private void Update()
    {
        EnemyMove();

        if (EnemyHasCooldown == false)
        {
            CurrentSpeed = MaxSpeed;
            StopCoroutine("EnemyAttackCooldown");
        }

        // Enemy Movement, Blend Tree mit allesn himmelrichtungen (Kein manueles setzten oder sprite flip)
    }

    protected virtual void EnemyMove()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= CurrentAttackRange/* && IsHittingWall == false*/)
        {
            Attack();
        }
        if (Vector3.Distance(Player.transform.position, transform.position) > CurrentAttackRange && GotHit == false)
        {
            MoveToStartPos();
        }
        if (GotHit == true)
        {
            if (Vector3.Distance(Player.transform.position, transform.position) <= CurrentAttackRange * AttackRangeModifier && IsHittingWall == false)
            {
                Attack();
            }
            else
            {
                MoveToStartPos();
            }
        }
        if (IsHittingWall == true)
        {
            MoveToStartPos();
        }
    }

    void MoveToStartPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, StartPos, 1 * Time.deltaTime);
        EnemyAnim.SetFloat("Horizontal", (StartPos.x - transform.position.x));
        EnemyAnim.SetFloat("Vertical", (StartPos.y - transform.position.y));

        if (transform.position.x == StartPos.x && transform.position.y == StartPos.y)
        {
            EnemyAnim.SetBool("IsMoving", false);

            IsHittingWall = false;
            CurrentSpeed = MaxSpeed;
        }

        //if (attackAudio.isPlaying)
        //{
        //    attackAudio.Stop();
        //}
    }



    protected void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, CurrentSpeed * Time.deltaTime);

        EnemyAnim.SetFloat("Horizontal", (Player.transform.position.x - transform.position.x));
        EnemyAnim.SetFloat("Vertical", (Player.transform.position.y - transform.position.y));
        EnemyAnim.SetBool("IsMoving", true);

        //if (!AttackAudio.isPlaying)
        //{
        //    AttackAudio.Play();
        //}
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
            Vector2 knockbackDirection = (Rb.transform.position - Player.transform.position).normalized;
            EnemyHasCooldown = true;
            Rb.AddForce(knockbackDirection * KnockbackForce);
            StartCoroutine("EnemyAttackCooldown");
        }

        if (collision.collider.CompareTag("Wall"))
        {
            IsHittingWall = true;
        }
    }


    IEnumerator EnemyAttackCooldown()
    {
        while (true)
        {
            if (EnemyHasCooldown == true)
            {
                Debug.Log("Enemy has cooldown");
                CurrentSpeed = 0;
                Rb.velocity = Vector2.zero;
                yield return new WaitForSeconds(1f);
                EnemyHasCooldown = false;
            }
            else
            {
                yield return null;
            }
        }
    }
}
