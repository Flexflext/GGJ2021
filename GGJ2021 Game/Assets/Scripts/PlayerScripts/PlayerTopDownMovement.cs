﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed;

    private Vector2 m_moveDir;
    private Vector2 CollisionDir;

    private Rigidbody2D m_rB;

    public SpriteRenderer PlayerSprite;

    public GameObject SwordPivot;

    [SerializeField] private GameObject Equipment;

    private Animator PlayerAnim;

    private AnimationClip[] animations;

    private bool CanMove = true;

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
        if (CanMove)
        {
            Move();
        }
    }

    private void ManageInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        m_moveDir = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        float movementStat = Game.Instance.PlayerManager.PlayerStat.GetStatValue(Attribute.MovementSpeed);
        float accel = m_MovementSpeed + movementStat;
        m_rB.velocity = new Vector2(m_moveDir.x * accel, m_moveDir.y * accel);
        float speedSqr = m_moveDir.sqrMagnitude;

        PlayerAnim.SetFloat("Speed", speedSqr);
        if (speedSqr > 0.01F)
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

    //private void OnCollisionEnter2D(Collision2D _collision)
    //{
    //    if (_collision.gameObject.tag == "Enemy")
    //    {
    //        CanMove = false;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D _collision)
    //{
    //    if (_collision.gameObject.tag == "Enemy")
    //    {
    //        CanMove = true;
    //    }
    //}
}