using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float DamageMultiplier;

    public float CurrentAttackDamage;
    public float CurrentAttackRange;

    float CurrentAttackSpeed;

    WeaponStats Weapon;
    public LayerMask EnemyLayer;

    bool IsAttackingUpperRight;
    bool IsAttackingUpperLeft;

    bool IsAttackingLowerRight;
    bool IsAttackingLowerLeft;

    PlayerTopDownMovement Player;

    void Start()
    {
        Player = FindObjectOfType<PlayerTopDownMovement>();
        Weapon = GetComponentInChildren<WeaponStats>();

        CurrentAttackDamage = Weapon.Damage;
       // CurrentAttackSpeed = Weapon.AttackSpeed;
        CurrentAttackRange = Weapon.AttackRange;
    }

    private void Update()
    {
        PlayerAttack();

        PlayerAttackDirection();
    }


    void PlayerAttackDirection()
    {
        if (Player.SwordPivot.transform.rotation.z > 0 && Player.SwordPivot.transform.rotation.z < 90)
        {
            if (!Player.PlayerSprite.flipX)
            {
                //Debug.Log("Attack upper Right");

                IsAttackingUpperRight = true;


                IsAttackingUpperLeft = false;
                IsAttackingLowerLeft = false;
                IsAttackingLowerRight = false;
            }
            else if (Player.PlayerSprite.flipX)
            {
                //Debug.Log("Attack upper Left");

                IsAttackingUpperLeft = true;


                IsAttackingUpperRight = false;
                IsAttackingLowerLeft = false;
                IsAttackingLowerRight = false;
            }
        }
        if (Player.SwordPivot.transform.rotation.z < 0 && Player.SwordPivot.transform.rotation.z > -90)
        {
            if (!Player.PlayerSprite.flipX)
            {
                //Debug.Log("Attack lower Right");

                IsAttackingLowerRight = true;


                IsAttackingUpperRight = false;
                IsAttackingUpperLeft = false;
                IsAttackingLowerLeft = false;

            }
            else if (Player.PlayerSprite.flipX)
            {
                //Debug.Log("Attack lower Left");

                IsAttackingLowerLeft = true;


                IsAttackingLowerRight = false;
                IsAttackingUpperRight = false;
                IsAttackingUpperLeft = false;
            }
        }
        //if (Player.SwordPivot.transform.rotation.z > 90 && Player.SwordPivot.transform.rotation.z < 180)
        //{
        //    Debug.Log("Attack upper Left");

        //    //IsAttackingUpperLeft = true;

        //    //IsAttackingUpperRight = false;

        //    //IsAttackingLowerLeft = false;
        //    //IsAttackingLowerRight = false;
        //}
        //if (Player.SwordPivot.transform.rotation.z < 180 && Player.SwordPivot.transform.rotation.z < -90)
        //{
        //    Debug.Log("Attack lower Left");

        //    //IsAttackingLowerLeft = true;

        //    //IsAttackingUpperRight = false;
        //    //IsAttackingUpperLeft = false;

        //    //IsAttackingLowerRight = false;
        //}
    }


    void PlayerAttack()
    {
        if (CurrentAttackSpeed <= 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider2D[] enemiesToDamage;

            /* 
             * 90 u. 0 rechts oben
             * 0 u. -90 rechts unten
             * 
             * 180 u. 90 links oben
             * 180 u. -90 links unten
             */


            if (Player.PlayerSprite.flipX == false)
            {
                    enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                    CurrentAttackSpeed = Weapon.AttackSpeed;
                    //Debug.Log("I attacked right");
            }
            else
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                CurrentAttackSpeed = Weapon.AttackSpeed;
                //Debug.Log("I attacked left");
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
                            enemy.GetComponent<Enemy_MOCK>().EnemyTakesDamage(CurrentAttackDamage);
                        }
                    }
                    else if (IsAttackingLowerRight)
                    {
                        Debug.Log("Lower Right");
                        if (enemy.transform.position.y < transform.position.y)
                        {
                            enemy.GetComponent<Enemy_MOCK>().EnemyTakesDamage(CurrentAttackDamage);
                        }
                    }
                }
                else if(enemy.transform.position.x < transform.position.x && Player.PlayerSprite.flipX)
                {
                    if (IsAttackingUpperLeft)
                    {
                        Debug.Log("Upper Left");

                        if (enemy.transform.position.y >= transform.position.y)
                        {
                            enemy.GetComponent<Enemy_MOCK>().EnemyTakesDamage(CurrentAttackDamage);
                        }
                    }
                    else if (IsAttackingLowerLeft)
                    {
                        Debug.Log("Lower Left");

                        if (enemy.transform.position.y < transform.position.y)
                        {
                            enemy.GetComponent<Enemy_MOCK>().EnemyTakesDamage(CurrentAttackDamage);
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
            CurrentAttackSpeed -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CurrentAttackRange);

        //Gizmos.DrawCube(Weapon.transform.position,  Weapon.WeaponBounds);
        //Gizmos.matrix = Matrix4x4.TRS(Weapon.transform.position, Quaternion.Euler(Weapon.transform.rotation.x, Weapon.transform.rotation.y, Weapon.transform.rotation.z), Vector3.one);
        //Gizmos.DrawWireCube(Weapon.transform.position, Weapon.WeaponBounds);

    }

}
