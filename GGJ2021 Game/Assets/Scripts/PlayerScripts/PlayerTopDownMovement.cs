using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    public float MovementSpeedMultiplier;

    [SerializeField]
    private float m_MovementSpeed;

    private Vector2 m_moveDir;

    private Rigidbody2D m_rB;

    SpriteRenderer PlayerSprite;

    Camera PlayerCam;

    public GameObject SwordPivot;

    private void Awake()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerCam = GetComponentInChildren<Camera>();
        m_rB = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        ManageInputs();

        FlipPlayer();
    }

    private void FixedUpdate()
    {
        Move();

    }

    void FlipPlayer()
    {
        //if (Input.GetAxisRaw("Horizontal") == -1)
        //{
        //    PlayerSprite.flipX = true;
        //}
        //else
        //{
        //    PlayerSprite.flipX = false;
        //}

        Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (lookDir.x < 0)
        {
            PlayerSprite.flipX = true;
        }
        else
        {
            PlayerSprite.flipX = false;
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
        m_rB.velocity = new Vector2(m_moveDir.x * m_MovementSpeed, m_moveDir.y * m_MovementSpeed);
    }
}
