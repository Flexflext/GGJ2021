using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public bool IsHittingWall;

    private void FixedUpdate()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            if (collision.gameObject.CompareTag("Wall"))
                IsHittingWall = true;

            Debug.Log("IS HITTING WALL WITH " + gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsHittingWall = false;
        Debug.Log("IS NOT HITTING WALL");
    }
}
