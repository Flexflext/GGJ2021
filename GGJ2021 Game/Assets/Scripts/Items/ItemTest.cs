using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ItemTest : MonoBehaviour
{
    [SerializeField] private ItemGenerator generator;

    void Update()
    {
        if (true) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            var items = generator.GenerateItems(transform, 100);
            foreach (var item in items)
            {
                item.transform.position += new Vector3(
                    Random.Range(-10F, 10F),
                    Random.Range(-10F, 10F),
                    0
                );
            }
        }
    }
}