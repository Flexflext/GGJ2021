using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy_BASE : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    private float CurrentHealth;

    [SerializeField] float MaxDamage;
    private float CurrentDamage;

    [SerializeField] float MaxAttackSpeed;
    private float CurrentAttackSpeed;

    [SerializeField] float MaxSpeed;
    private float CurrentSpeed;

    [SerializeField] float MaxAttackRange;
    private float CurrentAttackRange;

    [SerializeField] float AttackRangeModifier;

    public GameObject EnemyDeathAnim;

    PlayerHealth Player;

    Animator EnemyAnim;

    bool EnemyHasCooldown;

    Vector2 StartPos;

    Rigidbody2D Rb;

    bool GotHit;
    bool IsHittingWall;

    [SerializeField]
    float KnockbackForce;

    [SerializeField]
    float BubbleExtend;

    [SerializeField]
    LayerMask BlockingLayer;

    Vector3Int CellPosition;
    Vector3 CellCenterPosition;

    float CellSize;

    void Start()
    {
        EnemyDeathAnim = GetComponent<GameObject>();
        Player = FindObjectOfType<PlayerHealth>();
        EnemyAnim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();

        CurrentHealth = MaxHealth;
        CurrentAttackSpeed = MaxAttackSpeed;
        CurrentDamage = MaxDamage;
        CurrentSpeed = MaxSpeed;
        CurrentAttackRange = MaxAttackRange;

        StartPos = transform.position;
    }

    private void Update()
    {
        EnemyMove();

        if (EnemyHasCooldown == false)
        {
            CurrentSpeed = MaxSpeed;
            StopCoroutine("EnemyAttackCooldown");
        }

        // Enemy Movement, Blend Tree mit allesn himmelrichtungen (Kein manueles setzten oder sprite flip)
    }




    protected virtual void EnemyMove()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= CurrentAttackRange/* && IsHittingWall == false*/)
        {
            Attack();
        }
        //if (Vector3.Distance(Player.transform.position, transform.position) > CurrentAttackRange && GotHit == false)
        //{
        //    //MoveToStartPos();
        //}
        //if (GotHit == true)
        //{
        //    if (Vector3.Distance(Player.transform.position, transform.position) <= CurrentAttackRange * AttackRangeModifier && IsHittingWall == false)
        //    {
        //        Attack();
        //    }
        //    else
        //    {
        //        //MoveToStartPos();
        //    }
        //}
        //if (IsHittingWall == true)
        //{
        //    MoveToStartPos();
        //}
    }

    //void MoveToStartPos()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, StartPos, 1 * Time.deltaTime);

    //    EnemyAnim.SetFloat("Horizontal", (StartPos.x - transform.position.x));
    //    EnemyAnim.SetFloat("Vertical", (StartPos.y - transform.position.y));

    //    if (transform.position.x == StartPos.x && transform.position.y == StartPos.y)
    //    {
    //        EnemyAnim.SetBool("IsMoving", false);

    //        IsHittingWall = false;
    //        CurrentSpeed = MaxSpeed;
    //    }

    //    if (attackAudio.isPlaying)
    //    {
    //        attackAudio.Stop();
    //    }
    //}



    //protected void Attack()
    //{
    //    //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, CurrentSpeed * Time.deltaTime);

    //    EnemyMovement();

    //    EnemyAnim.SetFloat("Horizontal", (Player.transform.position.x - transform.position.x));
    //    EnemyAnim.SetFloat("Vertical", (Player.transform.position.y - transform.position.y));
    //    EnemyAnim.SetBool("IsMoving", true);

    //    //if (!AttackAudio.isPlaying)
    //    //{
    //    //    AttackAudio.Play();
    //    //}
    //}

    void Attack()
    {
        Collider2D[] Bubble = Physics2D.OverlapCircleAll(transform.position, BubbleExtend, BlockingLayer);


        //ContactPoint2D[] contacts = new ContactPoint2D[10];

        //Bubble.GetContacts(contacts.collider);

        //Collision2D BubbleInfo = Bubble.GetContacts(contacts[1].collider);

        Debug.Log(Bubble.Length);

        if (Bubble.Length == 1)
        {

            Tilemap gridLayout = Bubble[0].transform.GetComponent<Tilemap>();

            CellPosition = gridLayout.WorldToCell(transform.position);

            CellCenterPosition = gridLayout.GetCellCenterWorld(CellPosition);


            //Vector3Int CellPosition = gridLayout.WorldToCell(transform.position);
            //Vector3 cellCenterPosition = gridLayout.GetCellCenterWorld(CellPosition);

            //Vector3 actualCellPosition = gridLayout.CellToWorld(CellPosition);

            CellSize = gridLayout.cellSize.x / 2;

        }

        Vector3 blockerRight = new Vector3(CellCenterPosition.x + CellSize, CellCenterPosition.y, CellCenterPosition.z);
        Vector3 blockerLeft = new Vector3(CellCenterPosition.x - CellSize, CellCenterPosition.y, CellCenterPosition.z);
        Vector3 blockerUp = new Vector3(CellCenterPosition.x, CellCenterPosition.y + CellSize, CellCenterPosition.z);
        Vector3 blockerDown = new Vector3(CellCenterPosition.x, CellCenterPosition.y - CellSize, CellCenterPosition.z);




        if (Bubble.Length == 1)
        {
            Debug.Log("Bubble active");

            if (Vector3.Distance(transform.position, blockerRight) <= CellSize ||
                Vector3.Distance(transform.position, blockerLeft) <= CellSize ||
                Vector3.Distance(transform.position, blockerUp) <= CellSize ||
                Vector3.Distance(transform.position, blockerDown) <= CellSize)
            {
                Debug.Log("DDDDDDD ");

                // Rechts und Links //
                if (blockerRight.x >= transform.position.x && transform.position.x > Player.transform.position.x && !IsHittingWall)
                {
                    // Rechts
                    //Debug.Log("Rechts");

                    if (Player.transform.position.y >= transform.position.y)
                    {
                        // Rechts Oben
                        Debug.Log("Rechts Oben");
                        IsHittingWall = true;

                        Rb.velocity = transform.up.normalized * CurrentSpeed;

                    }
                    else if (Player.transform.position.y < transform.position.y)
                    {
                        // Rechts Unten
                        Debug.Log("Rechts Unten");
                        IsHittingWall = true;

                        Rb.velocity = -transform.up.normalized * CurrentSpeed;

                    }
                }
                else
                    IsHittingWall = false;

                if (blockerLeft.x < transform.position.x && transform.position.x > Player.transform.position.x)
                {
                    // Links,

                    //Debug.Log("Links");

                    if (Player.transform.position.y > transform.position.y)
                    {
                        // Links Oben
                        Debug.Log("Links Oben");
                        IsHittingWall = true;

                        Rb.velocity = transform.up.normalized * CurrentSpeed;
                    }
                    else if (Player.transform.position.y < transform.position.y)
                    {
                        // Links Unten
                        Debug.Log("Links Unten");
                        IsHittingWall = true;

                        Rb.velocity = -transform.up.normalized * CurrentSpeed;
                    }
                }
                else
                    IsHittingWall = false;

                // Oben und Unten //

                if (blockerUp.y >= transform.position.y && transform.position.y > Player.transform.position.y && !IsHittingWall)
                {
                    // Oben
                    //Debug.Log("Oben");

                    if (Player.transform.position.x >= transform.position.x)
                    {
                        // Oben Rechts
                        Debug.Log("Oben Rechts");
                        IsHittingWall = true;

                        Rb.velocity = transform.right.normalized * CurrentSpeed;
                    }
                    else if (Player.transform.position.x < transform.position.x)
                    {
                        // Oben Links
                        Debug.Log("Oben Links");
                        IsHittingWall = true;

                        Rb.velocity = -transform.right.normalized * CurrentSpeed;
                    }
                }
                else
                    IsHittingWall = false;

                if (blockerDown.y < transform.position.y && transform.position.y < Player.transform.position.y)
                {
                    // Unten
                    Debug.Log("Unten");

                    if (Player.transform.position.x > transform.position.x)
                    {
                        // Unten Rechts
                        Debug.Log("Unten Rechts");
                        IsHittingWall = true;

                        Rb.velocity = transform.right.normalized * CurrentSpeed;
                    }
                    else if (Player.transform.position.x < transform.position.x)
                    {
                        // Unten Links
                        Debug.Log("Unten Links");
                        IsHittingWall = true;

                        Rb.velocity = -transform.right.normalized * CurrentSpeed;
                    }
                }
                else
                    IsHittingWall = false;
            }
        }
        else
        {
            Debug.Log("Hunting Player");
            Rb.velocity = Rb.velocity.normalized;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, CurrentSpeed * Time.deltaTime);
        }

    }


    public void EnemyTakesDamage(float _damage)
    {
        CurrentHealth -= _damage;



        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);

            GameObject deathAnim = Instantiate(EnemyDeathAnim, transform.position, transform.rotation);
            AnimatorStateInfo deathAnimLenght = deathAnim.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (deathAnimLenght.length <= 0)
            {
                Destroy(deathAnim);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHit = collision.collider.GetComponent<PlayerHealth>();


        //ContactPoint2D[] contacts = new ContactPoint2D[10];

        //collision.GetContacts(contacts);




        //Vector3 wallPos = gridLayout.CellToWorld(cellPosition);

        //Debug.Log(gridLayout);
        //Debug.Log(cellPosition);

        if (collision.collider.GetComponent<PlayerHealth>())
        {
            playerHit.PlayerTakesDamge(CurrentDamage);
            Vector2 knockbackDirection = (Rb.transform.position - Player.transform.position).normalized;
            EnemyHasCooldown = true;
            Rb.AddForce(knockbackDirection * KnockbackForce);
            StartCoroutine("EnemyAttackCooldown");
        }
    }


    IEnumerator EnemyAttackCooldown()
    {
        while (true)
        {
            if (EnemyHasCooldown == true)
            {
                Debug.Log("Enemy has cooldown");
                //CurrentSpeed = 0;
                //Rb.velocity = Vector2.zero;
                yield return new WaitForSeconds(1f);
                EnemyHasCooldown = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    //Triggers when player Enters AggroRangeTrigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyMove();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BubbleExtend);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CurrentAttackRange);
    }
}
