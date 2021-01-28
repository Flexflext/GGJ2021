using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationScript : MonoBehaviour
{
    public Vector3 lookDir;


    void Update()
    {
        RotateWeapon();
    }


    void RotateWeapon()
    {
        Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
