using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationScript : MonoBehaviour
{
    public Vector3 lookDir;

    PlayerAttackScript Player;

    void Update()
    {
        Player = FindObjectOfType<PlayerAttackScript>();
        RotateWeapon();
    }


    void RotateWeapon()
    {
        if (!Player.IsAttacking)
        {
            Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
