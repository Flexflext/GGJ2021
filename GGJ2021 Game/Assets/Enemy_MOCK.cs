using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MOCK : MonoBehaviour
{
    [SerializeField]
    float Health;


    public void EnemyTakesDamage(float _damage)
    {
        Health -= _damage;

        Debug.Log(gameObject.name +  " Takes " + _damage + " Damage");

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
