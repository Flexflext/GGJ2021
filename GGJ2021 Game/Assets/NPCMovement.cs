using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float npcSpeed;
    [SerializeField] private float playerCheckRadius;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform[] npcWaypoints;

    private Vector3[] waypointPositions;

    public bool playerIsNear;

    private bool wayPointSet;

    private Vector3 wayPoint;
    private int nextWayPointIndex;
    private bool isReversed;


    private InfoScriptNPCCanvas canvasInfo;

    private void Awake()
    {
        waypointPositions = new Vector3[npcWaypoints.Length];

        for (int i = 0; i < npcWaypoints.Length; i++)
        {
            waypointPositions[i] = npcWaypoints[i].position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasInfo = GetComponentInChildren<InfoScriptNPCCanvas>();
        nextWayPointIndex = Random.Range(0, waypointPositions.Length);
        this.transform.position = waypointPositions[nextWayPointIndex];
        wayPoint = transform.position;
    }

    // Update is called once per frame
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
    }

    private void Stop()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position, npcSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (!wayPointSet)
        {
            SearchNextWayPoint(nextWayPointIndex);
        }

        if (wayPointSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoint, npcSpeed * Time.deltaTime);
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
            return;
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
}
