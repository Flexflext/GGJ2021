using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public Transform Weapon;
    
    public LayerMask EnemyLayer;

    float CurrentAttackCooldown;
    private float CurrentAttackDamage;
    private float CurrentAttackRange;
    
    bool IsAttackingUpperRight;
    bool IsAttackingUpperLeft;

    bool IsAttackingLowerRight;
    bool IsAttackingLowerLeft;

    PlayerTopDownMovement Player;

    public bool IsAttacking;

    public Animator WeaponSwingAnim;

    AnimatorStateInfo AnimationInfo;

    Vector3 LookDir;

    float AnimSpeedModifier;

    void Start()
    {
        Player = FindObjectOfType<PlayerTopDownMovement>();
    }

    private void Update()
    {
        PlayerAttack();
        
        PlayerAttackDirection();

        AnimationInfo = WeaponSwingAnim.GetCurrentAnimatorStateInfo(0);

        IsAttacking = !AnimationInfo.IsName("Idle");
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
            if (LookDir.x >= transform.position.x)
            {
                IsAttackingUpperRight = true;

                IsAttackingUpperLeft = false;
                IsAttackingLowerLeft = false;
                IsAttackingLowerRight = false;
            }
            else if (LookDir.x < transform.position.x)
            {
                IsAttackingUpperLeft = true;

                IsAttackingUpperRight = false;
                IsAttackingLowerLeft = false;
                IsAttackingLowerRight = false;
            }
        }
        if (Player.SwordPivot.transform.rotation.z < 0 && Player.SwordPivot.transform.rotation.z > -90)
        {
            if (LookDir.x >= transform.position.x)
            {
                IsAttackingLowerRight = true;


                IsAttackingUpperRight = false;
                IsAttackingUpperLeft = false;
                IsAttackingLowerLeft = false;

            }
            else if (LookDir.x < transform.position.x)
            {
                IsAttackingLowerLeft = true;


                IsAttackingLowerRight = false;
                IsAttackingUpperRight = false;
                IsAttackingUpperLeft = false;
            }
        }
    }


    void PlayerAttack()
    {
        CurrentAttackDamage = 5F + Game.Instance.PlayerManager.PlayerStat.GetStatValue(ItemStat.Damage);
        CurrentAttackRange = 2F + Game.Instance.PlayerManager.PlayerStat.GetStatValue(ItemStat.AttackRange);
        float attackSpeed = 1.5F + Game.Instance.PlayerManager.PlayerStat.GetStatValue(ItemStat.AttackSpeed);
        float currentAttackCooldownSpeed = 1 / attackSpeed;
        WeaponSwingAnim.speed = attackSpeed;
        if (CurrentAttackCooldown <= 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider2D[] enemiesToDamage;

            //Debug.Log("HIT");
            //AudioManager.instance.PlaySound(PlayerAttackSound); // player attack sfx

            if (LookDir.x >= transform.position.x)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                CurrentAttackCooldown = currentAttackCooldownSpeed;
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
                CurrentAttackCooldown = currentAttackCooldownSpeed;
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

                        if (enemy.transform.position.y >= transform.position.y)
                        {
                            Debug.Log("Upper Right");

                            enemy.GetComponent<Enemy>().TakeDamage(CurrentAttackDamage);
                        }
                    }
                    else if (IsAttackingLowerRight)
                    {

                        if (enemy.transform.position.y < transform.position.y)
                        {
                            Debug.Log("Lower Right");

                            enemy.GetComponent<Enemy>().TakeDamage(CurrentAttackDamage);
                        }
                    }
                }
                else if(enemy.transform.position.x < transform.position.x && LookDir.x < transform.position.x)
                {
                    if (IsAttackingUpperLeft)
                    {

                        if (enemy.transform.position.y >= transform.position.y)
                        {
                            Debug.Log("Upper Left");

                            enemy.GetComponent<Enemy>().TakeDamage(CurrentAttackDamage);
                        }
                    }
                    else if (IsAttackingLowerLeft)
                    {

                        if (enemy.transform.position.y < transform.position.y)
                        {                        Debug.Log("Lower Left");


                            enemy.GetComponent<Enemy>().TakeDamage(CurrentAttackDamage);
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CurrentAttackRange);
    }
}
