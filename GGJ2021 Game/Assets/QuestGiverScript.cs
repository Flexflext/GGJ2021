using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverScript : MonoBehaviour
{
    [SerializeField] private float npcSpeed;
    [SerializeField] private float playerCheckRadius;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private GameObject waypointHolder;

    private Vector3[] waypointPositions;

    public bool playerIsNear;

    private bool wayPointSet;

    private Vector3 wayPoint;
    private int nextWayPointIndex;
    private bool isReversed;

    private InfoScriptNPCCanvas canvasInfo;
    public Item item;

    private void Awake()
    {
        canvasInfo = GetComponentInChildren<InfoScriptNPCCanvas>();

        int waypointCount = waypointHolder.transform.childCount;
        waypointPositions = new Vector3[waypointCount];
        for (int i = 0; i < waypointCount; i++)
        {
            Transform waypointTransform = waypointHolder.transform.GetChild(i).GetComponent<Transform>();
            waypointPositions[i] = waypointTransform.position;
        }
    }

    void Start()
    {
        if (waypointPositions.Length > 0)
        {
            nextWayPointIndex = Random.Range(0, waypointPositions.Length);
            transform.position = waypointPositions[nextWayPointIndex];
            wayPoint = transform.position;
        }
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, playerCheckRadius, playerLayer))
        {
            playerIsNear = true;
            canvasInfo.NPCui.gameObject.SetActive(true);
        }
        else
        {
            playerIsNear = false;
            canvasInfo.NPCui.gameObject.SetActive(false);
        }

        if (!playerIsNear)
        {
            Move();
        }
        else
        {
            Stop();
        }

        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            Game.Instance.PlayerManager.Backpack.DestroyItem(item);
            Game.Instance.PlayerManager.PlayerStat.money += 1;
        }
    }

    private void Stop()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position, npcSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (wayPointSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoint, npcSpeed * Time.deltaTime);
        }
        else
        {
            SearchNextWayPoint(nextWayPointIndex);
        }

        if (transform.position == wayPoint)
        {
            wayPointSet = false;
        }
    }

    /// <summary>
    /// Looks for the Next WayPoint of the Enemy in the WayPoint Array, goes in reverse steps if the Arraylenght is reached.
    /// </summary>
    /// <param name="_waypointindex"> Current index of the Array. </param>
    private void SearchNextWayPoint(int _waypointindex)
    {
        if (waypointPositions.Length == 0) return;
        if (_waypointindex < waypointPositions.Length && !isReversed)
        {
            wayPoint = waypointPositions[_waypointindex];

            nextWayPointIndex++;
            if (nextWayPointIndex == waypointPositions.Length)
            {
                isReversed = true;
                nextWayPointIndex--;
            }

            wayPointSet = true;
        }
        else if (_waypointindex < waypointPositions.Length && isReversed)
        {
            wayPoint = waypointPositions[_waypointindex];

            nextWayPointIndex--;
            if (nextWayPointIndex == 0)
            {
                isReversed = false;
            }

            wayPointSet = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        canvasInfo.NameText.text = item.Name;
        canvasInfo.ItemSprite.sprite = item.Icon;
        canvasInfo.NameText.color = item.Rarity.NameColor;
    }
}