using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualPlayer : Unit
{
    Vector2 m_Input;
    private void Update()
    {
        ManageInputs();
        Move(Vector2.zero);
    }
    protected override void Move(Vector2 _targetposition)
    {
        m_NavMeshAgent.Move((m_MoveDirection * Time.deltaTime));
        SetAnimatorValues();
    }

    private void ManageInputs()
    {
        m_Input.x = Input.GetAxisRaw("Horizontal");
        m_Input.y = Input.GetAxisRaw("Vertical");

        m_Input.Normalize();
        if (m_Input != Vector2.zero)
        {
            m_MoveDirection = m_Input;
        }
    }
}
