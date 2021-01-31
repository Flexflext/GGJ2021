using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationScript : MonoBehaviour
{
    Vector3 LookDir;

    PlayerAttackScript Player;

    SpriteRenderer WeaponSprite;

    void Update()
    {
        Player = FindObjectOfType<PlayerAttackScript>();
        RotateWeapon();

        WeaponSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (LookDir.x >= Player.transform.position.x)
        {
            WeaponSprite.flipY = false;
        }
        else if (LookDir.x < Player.transform.position.x)
        {
            WeaponSprite.flipY = true;
        }
    }


    void RotateWeapon()
    {
        if (!Player.IsAttacking)
        {
            LookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        }
    }
}
