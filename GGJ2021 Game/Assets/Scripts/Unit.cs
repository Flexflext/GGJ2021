using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] protected float m_MaxHealth;
    public float MaxHealth { get { return m_MaxHealth + Stats[(int)Attribute.Health]; } }
    protected float m_Health;
    public float Health { get; set; }
    [SerializeField] protected float m_AttackPower;
    public float AttackPower { get { return m_AttackPower + Stats[(int)Attribute.AttackPower]; } }
    [SerializeField] protected float m_MoveSpeed;
    public float MoveSpeed { get { return m_MoveSpeed + Stats[(int)Attribute.MovementSpeed]; } }
    [SerializeField] protected float m_AttackSpeed;
    public float AttackSpeed { get { return m_AttackSpeed + Stats[(int)Attribute.AttackSpeed]; } }
    protected Animator m_Animator;
    [SerializeField] protected Sound m_AttackSound;
    [SerializeField] protected Sound m_WalkSound;
    [SerializeField] protected Sound m_HitSound;
    [SerializeField] protected Sound m_DeathSound;
    protected bool m_Attacked;
    protected bool m_IsMoving;
    protected WaitForSeconds m_AttckSpeedCooldown;
    protected Vector2 m_MoveDirection;
    protected NavMeshAgent m_NavMeshAgent;
    protected Vector2 m_StartPosition;
    protected Vector2 m_Destination;
    protected int[] Stats = new int[System.Enum.GetValues(typeof(Attribute)).Length];

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_NavMeshAgent.updateRotation = false;
        m_NavMeshAgent.updateUpAxis = false;
        m_StartPosition = transform.position;
        m_Destination = transform.position;
        m_Animator = GetComponentInChildren<Animator>();
        m_Attacked = false;
        m_AttckSpeedCooldown = new WaitForSeconds(1 / m_AttackSpeed);
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] = 0;
        }
    }

    public void Damage(float _damage)
    {
        Health -= _damage;
        m_Animator.SetTrigger("IsDamaged");

        if (Health <= 0)
        {
            StartCoroutine(CDeath());
        }
        AudioManager.instance.PlaySound(m_HitSound);
    }

    protected virtual void Attack(Unit _unit)
    {
        if (_unit != null)
        {
            _unit.Damage(m_AttackPower);
            StartCoroutine(CAttackCooldown());
        }
    }

    protected virtual void Move(Vector2 _targetposition)
    {
        m_NavMeshAgent.SetDestination(_targetposition);
        if (m_NavMeshAgent.speed > 0)
        {
            m_IsMoving = true;
            SetAnimatorValues();
        }
        else
        {
            m_IsMoving = false;
            SetAnimatorValues();
        }

    }

    protected virtual void SetAnimatorValues()
    {
        m_Animator.SetBool("IsMoving", m_IsMoving);
        m_Animator.SetFloat("Horizontal", m_MoveDirection.x);
        m_Animator.SetFloat("Vertical", m_MoveDirection.y);
    }

    protected IEnumerator CAttackCooldown()
    {
        m_Attacked = true;
        yield return m_AttckSpeedCooldown;
        m_Attacked = false;
    }

    protected IEnumerator CMoveToSpawnPosition()
    {
        m_Destination = m_StartPosition;
        while (m_IsMoving)
        {
            Move(m_Destination);
            yield return new WaitForEndOfFrame();
        }
        transform.position = m_Destination;
        yield break;
    }

    protected IEnumerator CDeath()
    {
        AnimatorStateInfo stateinfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        AudioManager.instance.PlaySound(m_DeathSound);
        m_Animator.SetTrigger("IsDead");
        while (stateinfo.length > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        //TODO: implement POOLING
        Destroy(gameObject, m_Animator.GetCurrentAnimatorStateInfo(0).length);
    }

    protected virtual void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.tag == "Player")
        {
            StopCoroutine(CMoveToSpawnPosition());
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.tag == "Player")
        {
            m_Destination = _collision.transform.position;
            Move(_collision.transform.position);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.tag == "Player")
        {
            StartCoroutine(CMoveToSpawnPosition());
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, m_Destination);
    }

    protected Vector2 Position { get { return this.transform.position; } }
}
