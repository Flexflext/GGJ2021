using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float MaxAttackSpeed;
    private float AttackSpeed;
    private WaitForSeconds AttckSpeedCooldown;

    [SerializeField] float MaxHealth = 10;
    private float Health;

    [SerializeField] float MaxDamage = 2;
    private float Damage;

    [SerializeField] float MaxSpeed;
    private float Speed = 2;

    [SerializeField] float AttackRangeModifier = 2;


    public GameObject EnemyDeathAnim;

    private PlayerHealth Player;

    private Animator EnemyAnim;

    private bool Attacked;
    private bool IsHittingWall;
    private bool GotHit;


    private Vector2 StartPos;

    private Rigidbody2D Rigidbody2D;


    [SerializeField]
    private float KnockbackForce;


    [SerializeField] private Sound enemyHitSound;
    [SerializeField] private Sound enemyDeathSound;


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerHealth>();
        EnemyAnim = GetComponentInChildren<Animator>();

        StartPos = transform.position;
        Attacked = false;

        AttckSpeedCooldown = new WaitForSeconds(3 / AttackSpeed);
    }

    protected virtual void Move(Vector3 _pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, _pos, Speed * Time.deltaTime);

        SetAnimatorValues(_pos);
    }

    protected virtual void Attack(PlayerHealth _playerhealth)
    {
        if (_playerhealth)
        {
            _playerhealth.PlayerTakesDamge(Damage);
            StartCoroutine("AttackCooldown");
        }
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        EnemyAnim.SetTrigger("IsDamaged");

        if (Health <= 0)
        {
            AudioManager.instance.PlaySound(enemyDeathSound);
            Destroy(gameObject);

            GameObject deathAnim = Instantiate(EnemyDeathAnim, transform.position, transform.rotation);
        }
        AudioManager.instance.PlaySound(enemyHitSound);
    }

    IEnumerator AttackCooldown()
    {
        Attacked = true;
        Speed -= Speed;

        yield return AttckSpeedCooldown;

        Attacked = false;
        Speed = MaxSpeed;
        yield break;
    }

    private void SetAnimatorValues(Vector3 _pos)
    {
        EnemyAnim.SetFloat("Speed", Speed);

        if (_pos.x > transform.position.x)
        {
            EnemyAnim.SetFloat("Horizontal", 1);
            EnemyAnim.SetFloat("Vertical", 0);
        }
        if (_pos.x < transform.position.x)
        {
            EnemyAnim.SetFloat("Horizontal", -1);
            EnemyAnim.SetFloat("Vertical", 0);
        }
        if (_pos.y > transform.position.y)
        {
            EnemyAnim.SetFloat("Vertical", 1);
            EnemyAnim.SetFloat("Horizontal", 0);
        }
        if (_pos.y < transform.position.y)
        {
            EnemyAnim.SetFloat("Vertical", -1);
            EnemyAnim.SetFloat("Horizontal", 0);
        }
    }

    #region Collision and Trigger Events

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Attack(_collision.gameObject.GetComponent<PlayerHealth>());
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Move(_collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Move(StartPos);
        }
    }
    #endregion
}