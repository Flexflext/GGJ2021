using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            var itemComponent = (Item)collision.gameObject.GetComponent(typeof(Item));
            itemComponent.OnPickup(gameObject);

            Destroy(collision.gameObject);
        }
    }
}
