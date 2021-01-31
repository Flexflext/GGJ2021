using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float CurrentAttackDamage;
    public float CurrentAttackRange;

    float CurrentAttackCooldown;
    float CurrentAttackSpeed;

    WeaponStats Weapon;
    public LayerMask EnemyLayer;

    bool IsAttackingUpperRight;
    bool IsAttackingUpperLeft;

    bool IsAttackingLowerRight;
    bool IsAttackingLowerLeft;

    PlayerTopDownMovement Player;

    public bool IsAttacking;

    public Animator WeaponSwingAnim;

    AnimatorStateInfo AnimationInfo;

    public Sound PlayerAttackSound;

    Vector3 LookDir;

    void Start()
    {
        Player = FindObjectOfType<PlayerTopDownMovement>();
        Weapon = GetComponentInChildren<WeaponStats>();

        CurrentAttackDamage = Weapon.Damage;
        CurrentAttackSpeed = Weapon.AttackSpeed;
        CurrentAttackRange = Weapon.AttackRange;

        WeaponSwingAnim.speed = WeaponSwingAnim.speed += CurrentAttackSpeed;
    }

    private void Update()
    {
        PlayerAttack();
        
        PlayerAttackDirection();

        AnimationInfo = WeaponSwingAnim.GetCurrentAnimatorStateInfo(0);

        if (!AnimationInfo.IsName("Idle"))
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }
    }
    private void FixedUpdate()
    {
        GetMousePos();
    }

    void GetMousePos()
    {
        LookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }

    void PlayerAttackDirection()
    {
        if (Player.SwordPivot.transform.rotation.z > 0 && Player.SwordPivot.transform.rotation.z < 90)
        {
            Debug.Log("DDDDDDDDDD");
            if (LookDir.x >= transform.position.x)
            {
               Debug.Log("Attack upper Right");

                IsAttackingUpperRight = true;


                IsAttackingUpperLeft = false;
                IsAttackingLowerLeft = false;
                IsAttackingLowerRight = false;
            }
            else if (LookDir.x < transform.position.x)
            {
                Debug.Log("Attack upper Left");

                IsAttackingUpperLeft = true;


                IsAttackingUpperRight = false;
                IsAttackingLowerLeft = false;
                IsAttackingLowerRight = false;
            }
        }
        if (Player.SwordPivot.transform.rotation.z < 0 && Player.SwordPivot.transform.rotation.z > -90)
        {
            Debug.Log("HHHHHHHH");

            if (LookDir.x >= transform.position.x)
            {
                Debug.Log("Attack lower Right");

                IsAttackingLowerRight = true;


                IsAttackingUpperRight = false;
                IsAttackingUpperLeft = false;
                IsAttackingLowerLeft = false;

            }
            else if (LookDir.x < transform.position.x)
            {
                Debug.Log("Attack lower Left");

                IsAttackingLowerLeft = true;


                IsAttackingLowerRight = false;
                IsAttackingUpperRight = false;
                IsAttackingUpperLeft = false;
            }
        }
    }


    void PlayerAttack()
    {
        if (CurrentAttackCooldown <= 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider2D[] enemiesToDamage;

            //AudioManager.instance.PlaySound(PlayerAttackSound); // player attack sfx

            if (LookDir.x >= transform.position.x)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                CurrentAttackCooldown = Weapon.AttackCooldownSpeed;
                //Debug.Log("I attacked right");

                if (IsAttackingUpperRight && !IsAttacking)
                {
                    WeaponSwingAnim.SetTrigger("IsAttackingUpperRight");

                }
                else if (IsAttackingLowerRight && !IsAttacking)
                {
                    WeaponSwingAnim.SetTrigger("IsAttackingLowerRight");

                }
            }
            else
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                CurrentAttackCooldown = Weapon.AttackCooldownSpeed;
                //Debug.Log("I attacked left");

                if (IsAttackingUpperLeft && !IsAttacking)
                {
                    WeaponSwingAnim.SetTrigger("IsAttackingUpperLeft");

                }
                else if (IsAttackingLowerLeft && !IsAttacking)
                {
                    WeaponSwingAnim.SetTrigger("IsAttackingLowerLeft");

                }
            }
            foreach (Collider2D enemy in enemiesToDamage)
            {
                if (enemy.transform.position.x >= transform.position.x && !Player.PlayerSprite.flipX)
                {
                    if (IsAttackingUpperRight)
                    {
                        Debug.Log("Upper Right");

                        if (enemy.transform.position.y >= transform.position.y)
                        {

                            enemy.GetComponent<Enemy_BASE>().EnemyTakesDamage(CurrentAttackDamage);

                        }
                    }
                    else if (IsAttackingLowerRight)
                    {
                        Debug.Log("Lower Right");

                        if (enemy.transform.position.y < transform.position.y)
                        {

                            enemy.GetComponent<Enemy_BASE>().EnemyTakesDamage(CurrentAttackDamage);

                        }
                    }
                }
                else if(enemy.transform.position.x < transform.position.x && LookDir.x < transform.position.x)
                {
                    if (IsAttackingUpperLeft)
                    {
                        Debug.Log("Upper Left");

                        if (enemy.transform.position.y >= transform.position.y)
                        {

                            enemy.GetComponent<Enemy_BASE>().EnemyTakesDamage(CurrentAttackDamage);

                        }
                    }
                    else if (IsAttackingLowerLeft)
                    {
                        Debug.Log("Lower Left");

                        if (enemy.transform.position.y < transform.position.y)
                        {

                            enemy.GetComponent<Enemy_BASE>().EnemyTakesDamage(CurrentAttackDamage);

                        }
                    }
                }
            }

            //PlayerAnim.SetTrigger("IsAttacking");
            //PlayerSFX.EnemyAttacks();
            //CurrentAttackSpeed = Weapon.AttackSpeed;
        }
        else
        {
            CurrentAttackCooldown -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CurrentAttackRange);
    }

}
