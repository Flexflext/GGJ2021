using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ItemTest : MonoBehaviour
{
    [SerializeField] private ItemGenerator generator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var items = generator.GenerateItems(100);
            foreach (var item in items)
            {
                item.transform.SetParent(transform);
                item.transform.position += new Vector3(
                    Random.Range(-10F, 10F),
                    Random.Range(-10F, 10F),
                    0
                );
            }
        }
    }
}