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

        //PlayerAnim.SetInteger("Direction", GetDirection(m_rB.velocity));
        PlayerAnim.SetFloat("Horizontal", m_moveDir.x);
        PlayerAnim.SetFloat("Vertical", m_moveDir.y);
        PlayerAnim.SetFloat("Speed", m_moveDir.sqrMagnitude);

        // AnimatorStateInfo currentState = PlayerAnim.GetCurrentAnimatorStateInfo(0);
        // Debug.Log(currentState);
    }

    private int GetDirection(Vector2 v)
    {
        if (v.x > 0) return 3;
        if (v.x < 0) return 1;
        if (v.y > 0) return 2;
        return 0;
    }
}