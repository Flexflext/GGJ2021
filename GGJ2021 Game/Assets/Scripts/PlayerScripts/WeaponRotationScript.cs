using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationScript : MonoBehaviour
{
    Vector3 LookDir;

    [SerializeField] private PlayerAttackScript PlayerAttackScript;

    SpriteRenderer WeaponSprite;

    void Update()
    {
        RotateWeapon();

        WeaponSprite = GetComponentInChildren<SpriteRenderer>(true);
    }

    private void FixedUpdate()
    {        if (!WeaponSprite) return;
        if (LookDir.x >= PlayerAttackScript.transform.position.x)
        {
            WeaponSprite.flipY = false;
        }
        else if (LookDir.x < PlayerAttackScript.transform.position.x)
        {
            WeaponSprite.flipY = true;
        }
    }


    void RotateWeapon()
    {
        if (!PlayerAttackScript.IsAttacking)
        {
            LookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}