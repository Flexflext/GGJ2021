using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToRoomTrigger : MonoBehaviour
{
    private const int XOFFSET = 10;
    private const int YOFFSET = 10;
    private WaitForSeconds TriggerTimerInterval = new WaitForSeconds(0.3f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Game.Instance.PlayerManager.RecentlyTeleported)
        {
            switch (gameObject.tag)
            {
                case "ToDungeon":
                    var dungeonPos = new Vector2(0, 200);
                    collision.gameObject.transform.position = new Vector3(dungeonPos.x - 0.5F, dungeonPos.y - 4.5F, 0);
                    AudioManager.instance.ChangeBackgroundMusic(AudioManager.EBackgroundMusicThemes.Dungeon);
                    StartCoroutine("TriggerTimer");
                    break;
                case "Up":
                    TeleportPlayer(new Vector3(0, YOFFSET, 0), collision.transform);
                    break;
                case "Down":
                    DungeonRoom dungeonRoom = gameObject.transform.parent.GetComponent<DungeonRoom>();
                    if (dungeonRoom.IsStart)
                    {
                        collision.transform.position = new Vector3(-6, 14.2F, 0);
                        Game.Instance.DungeonGenerator.GenerateDungeon();
                        AudioManager.instance.ChangeBackgroundMusic(AudioManager.EBackgroundMusicThemes.Hub);
                        StartCoroutine("TriggerTimer");
                    }
                    else
                    {
                        TeleportPlayer(new Vector3(0, -YOFFSET, 0), collision.transform);
                    }

                    break;
                case "Left":
                    TeleportPlayer(new Vector3(-XOFFSET, 0, 0), collision.transform);
                    break;
                case "Right":
                    TeleportPlayer(new Vector3(XOFFSET, 0, 0), collision.transform);
                    break;
                default:
                    Debug.LogError("Could Not Move Player(No tag FOund)");
                    break;
            }
        }
    }

    private void TeleportPlayer(Vector3 _offset, Transform _transfrom)
    {
        _transfrom.position += _offset;
        StartCoroutine("TriggerTimer");
    }

    private IEnumerator TriggerTimer()
    {
        if (Game.Instance.PlayerManager.RecentlyTeleported) yield break;
        Game.Instance.PlayerManager.RecentlyTeleported = true;
        yield return TriggerTimerInterval;
        Game.Instance.PlayerManager.RecentlyTeleported = false;
    }
}