using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float MaxAttackSpeed;
    private float AttackSpeed = 2;
    private WaitForSeconds AttckSpeedCooldown;

    [SerializeField] float MaxHealth = 10;
    private float Health;

    [SerializeField] float MaxDamage = 2;
    private float Damage;

    [SerializeField] float MaxSpeed;
    private float Speed = 3;

    [SerializeField] float AttackRangeModifier = 2;


    public GameObject EnemyDeathAnim;

    private PlayerHealth Player;

    private Animator EnemyAnim;

    private bool Attacked;
    private bool IsHittingWall;
    private bool GotHit;


    private Vector3 StartPos;
    private Vector3 MoveDir;
    private Vector3 Destination;

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

    private void Update()
    {
        SetMoveDir(Destination);
    }

    //protected virtual void Move(Vector2 _pos)
    //{
    //    Rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position, _pos, Speed * Time.deltaTime));
    //    SetAnimatorValues(_pos);
    //}

    private IEnumerator CMove()
    {
        while (this.transform.position != Destination)
        {
            Rigidbody2D.MovePosition(Vector2.MoveTowards(this.transform.position, Destination, Speed * Time.deltaTime));
            SetAnimatorValues();
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetMoveDir(Vector3 _destination)
    {
        MoveDir = this.transform.position - _destination;
    }

    protected virtual void Attack(PlayerHealth _playerhealth)
    {
        if (_playerhealth)
        {
            _playerhealth.PlayerTakesDamge(Damage);
            StartCoroutine(CAttackCooldown());
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

    IEnumerator CAttackCooldown()
    {
        Attacked = true;
        yield return AttckSpeedCooldown;
        Attacked = false;
    }

    private void SetAnimatorValues()
    {
        EnemyAnim.SetFloat("Speed", MoveDir.magnitude);

        if (Destination.x > transform.position.x)
        {
            EnemyAnim.SetFloat("Horizontal", 1);
            EnemyAnim.SetFloat("Vertical", 0);
        }
        if (Destination.x < transform.position.x)
        {
            EnemyAnim.SetFloat("Horizontal", -1);
            EnemyAnim.SetFloat("Vertical", 0);
        }
        if (Destination.y > transform.position.y)
        {
            EnemyAnim.SetFloat("Vertical", 1);
            EnemyAnim.SetFloat("Horizontal", 0);
        }
        if (Destination.y < transform.position.y)
        {
            EnemyAnim.SetFloat("Vertical", -1);
            EnemyAnim.SetFloat("Horizontal", 0);
        }
    }

    #region Collision and Trigger Events

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Attack(_collision.collider.GetComponent<PlayerHealth>());
        }
    }


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Destination = _collision.transform.position;
            StartCoroutine(CMove());
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Destination = _collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Destination = StartPos;
        }
    }
    #endregion
}