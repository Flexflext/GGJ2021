using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnim : MonoBehaviour
{
    Animator DeathAnim;
    AnimatorStateInfo DeathAnimLenght;

    // Start is called before the first frame update
    void Start()
    {
        DeathAnim = GetComponent<Animator>();

        DeathAnim.SetTrigger("IsActive");

        //DeathAnimLenght = DeathAnim.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (DeathAnimLenght.length <= 0)
        {
            Destroy(gameObject, 2f);
        }
    }
}
