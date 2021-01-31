using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_GFX : MonoBehaviour
{
    Animator EnemyAnim;

    public AIPath EnemyAiPath;

    void Start()
    {
        EnemyAnim = GetComponent<Animator>();

        EnemyAiPath.enableRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        EnemyAnim.SetFloat("Horizontal", EnemyAiPath.desiredVelocity.x);
        EnemyAnim.SetFloat("Vertical", EnemyAiPath.desiredVelocity.y);
        EnemyAnim.SetFloat("Speed", EnemyAiPath.desiredVelocity.sqrMagnitude);

    }
}
