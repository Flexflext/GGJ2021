using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    [SerializeField]
    private float m_MovementSpeed;

    private Vector2 m_moveDir;
    
    private Rigidbody2D m_rB;

    private void Awake()
    {
        m_rB = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
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
    }
}
