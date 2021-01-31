using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed;

    private Vector2 m_moveDir;

    private Rigidbody2D m_rB;

    public SpriteRenderer PlayerSprite;

    public GameObject SwordPivot;

    [SerializeField] private GameObject Equipment;

    private Animator PlayerAnim;

    private AnimationClip[] animations;

    private void Awake()
    {
        PlayerAnim = GetComponent<Animator>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        m_rB = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        ManageInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ManageInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        m_moveDir = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        m_rB.velocity = new Vector2(m_moveDir.x * m_MovementSpeed, m_moveDir.y * m_MovementSpeed);
        float speed = m_moveDir.sqrMagnitude;

        PlayerAnim.SetFloat("Speed", speed);
        if (speed > 0.01F)
        {
            PlayerAnim.SetFloat("Horizontal", m_moveDir.x);
            PlayerAnim.SetFloat("Vertical", m_moveDir.y);
            PlayerAnim.SetBool("IsWalking", true);
        }
        else
        {
            PlayerAnim.SetBool("IsWalking", false);
        }
    }
}