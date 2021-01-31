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
                case "Up":
                    TeleportPlayer(new Vector3(0, YOFFSET, 0), collision.transform);
                    break;
                case "Down":
                    DungeonRoom dungeonRoom = gameObject.transform.parent.GetComponent<DungeonRoom>();
                    if (dungeonRoom.IsStart)
                    {
                        collision.transform.position = new Vector3(-6, 14.2F, 0);
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
        Game.Instance.PlayerManager.RecentlyTeleported = true;
        yield return TriggerTimerInterval;
        Game.Instance.PlayerManager.RecentlyTeleported = false;
    }
}