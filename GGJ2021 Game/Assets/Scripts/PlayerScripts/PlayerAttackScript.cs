using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float CurrentAttackDamage;
    public float CurrentAttackSpeed;
    public float CurrentAttackRange;

    public WeaponStats Weapon;

    LayerMask EnemyLayer;

    void Start()
    {
        CurrentAttackDamage = Weapon.Damage;
        CurrentAttackSpeed = Weapon.AttackSpeed;
        CurrentAttackRange = Weapon.AttackRange;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }
    }

    void PlayerAttack()
    {
       

        if (CurrentAttackSpeed <= 0)
        {
            Collider2D[] enemiesToDamage;

            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                Debug.Log("I attacked right");

                //enemiesToDamage = Physics2D.OverlapBoxAll(Weapon.transform.position, Weapon.WeaponBounds, EnemyLayer);
            }
            else
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Weapon.transform.position, CurrentAttackRange, EnemyLayer);
                Debug.Log("I attacked left");


                //enemiesToDamage = Physics2D.OverlapBoxAll(Weapon.transform.position, Weapon.WeaponBounds, EnemyLayer);
            }
            foreach (Collider2D enemy in enemiesToDamage)
            {
                //enemy.GetComponent<EnemyBehaviour>().TakeDamage(CurrentDamaged);
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
