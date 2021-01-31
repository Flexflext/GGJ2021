using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToRoomTrigger : MonoBehaviour
{
    private const int XOFFSET = 9;
    private const int YOFFSET = 8;
    private WaitForSeconds TriggerTimerInterval = new WaitForSeconds(2f);
    private bool IsActive = false;

    public bool SetActive { set { IsActive = value; } }

    private void Awake()
    {
        IsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
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

    private void TeleportPlayer(Vector3 _pos, Transform _transfrom)
    {
        _transfrom.position += _pos;
        StartCoroutine("TriggerTimer");
    }

    private IEnumerator TriggerTimer()
    {
        IsActive = false;
        yield return TriggerTimerInterval;
        IsActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsActive = true;
    }
}
