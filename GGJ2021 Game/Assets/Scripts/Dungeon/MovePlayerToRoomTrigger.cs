using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToRoomTrigger : MonoBehaviour
{
    private const int XOFFSET = 11;
    private const int YOFFSET = 10;
    private bool IsActive = false;

    public bool SetActive { set { IsActive = value; } }

    private void Awake()
    {
        if (!this.GetComponentInParent<DungeonRoom>().IsStart)
        {
            IsActive = false;
        }
        else IsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            switch (this.gameObject.tag)
            {
                case "Up":
                    collision.gameObject.transform.position += new Vector3(0, YOFFSET, 0);
                    break;
                case "Down":
                    collision.gameObject.transform.position += new Vector3(0, -YOFFSET, 0);
                    break;
                case "Left":
                    collision.gameObject.transform.position += new Vector3(-XOFFSET, 0, 0);
                    break;
                case "Right":
                    collision.gameObject.transform.position += new Vector3(XOFFSET, 0, 0);
                    break;
                default:
                    Debug.LogError("Could Not Move Player(No tag FOund)");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsActive = true;
    }
}
