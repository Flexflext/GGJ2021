using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTeleporterScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.CompareTag("Player"))
        {
            var dungeonPos = new Vector2(0, 200);
            collision.gameObject.transform.position = new Vector3(dungeonPos.x - 0.5F, dungeonPos.y - 4.5F, 0);
            AudioManager.instance.ChangeBackgroundMusic(AudioManager.EBackgroundMusicThemes.Dungeon);
        }
    }
}