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
        if (collision.tag == "Player" && !Game.Instance.PlayerManager.RecentlyTeleported)
        {
            switch (this.gameObject.tag)
            {
                case "Up":
                    TeleportPlayer(new Vector3(0, YOFFSET, 0), collision.transform);
                    break;
                case "Down":
                    TeleportPlayer(new Vector3(0, -YOFFSET, 0), collision.transform);
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
