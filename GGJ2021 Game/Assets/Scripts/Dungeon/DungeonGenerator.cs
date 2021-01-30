using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DungeonRoom;
using static DungeonRoom.Connection;
using static UnityEngine.GameObject;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    private const int XOFFSET = 32;
    private const int YOFFSET = 22;

    [SerializeField] private Vector3 DungeonStartPos;
    [SerializeField] private Transform ParentTransform;
    [SerializeField] private int MinimalRoomAmount;
    [SerializeField] private GameObject[] DownRoomPrefabs;
    [SerializeField] private GameObject[] LeftRoomPrefabs;
    [SerializeField] private GameObject[] UpRoomPrefabs;
    [SerializeField] private GameObject[] RightRoomPrefabs;
    [SerializeField] private GameObject[] EndRoomsPrefabs;

    private List<DungeonRoom> OpenRooms = new List<DungeonRoom>();
    private List<DungeonRoom> DungeonRooms = new List<DungeonRoom>();

    private void Start()
    {
        GenerateDungeon();
    }

    [ContextMenu("GenerateDungeon")]
    public void GenerateDungeon()
    {
        foreach (Transform child in ParentTransform) {
            Destroy(child.gameObject);
        }
        DungeonRooms.Clear();
        OpenRooms.Clear();

        DungeonRoom startRoom = CreateRoom(Down, DungeonStartPos);
        startRoom.IsStart = true;

        while (OpenRooms.Count != 0)
        {
            for (int i = 0; i < OpenRooms.Count; i++)
            {
                AddRoom(OpenRooms[i]);
            }
        }
    }

    private void AddRoom(DungeonRoom _thisroom)
    {
        Vector3 roomPos = _thisroom.transform.position;
        for (int i = 0; i < _thisroom.Connections.Length; i++)
        {
            if (_thisroom.Origin == _thisroom.Connections[i]) continue;
            CreateRoom(_thisroom.Connections[i], roomPos);
        }

        OpenRooms.Remove(_thisroom);
    }

    private DungeonRoom CreateRoom(Connection origin, Vector3 originPos)
    {
        Vector3 newPos = GetAdjacentRoomPos(originPos, origin);
        var endRoom = DungeonRooms.Count <= MinimalRoomAmount;
        GameObject[] roomPrefabs = GetRoomPrefabs(InvertConnection(origin), endRoom);

        var existingRoom = GetRoomAt(newPos);
        if (existingRoom)
        {
            // A room already exists here, so we don't even have to start searching for a fitting one
            // Maybe check connections and throw error if something doesn't add up?
            return null;
        }

        GameObject rndRoomPrefab;
        do
        {
            rndRoomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
        } while (!DoesRoomFit(newPos, rndRoomPrefab, origin));

        GameObject room = Instantiate(rndRoomPrefab, newPos, Quaternion.identity, ParentTransform);
        DungeonRoom droom = room.GetComponent<DungeonRoom>();
        droom.Origin = origin;

        DungeonRooms.Add(droom);
        OpenRooms.Add(droom);
        return droom;
    }

    private bool DoesRoomFit(Vector3 wantedPos, GameObject rndRoom, Connection origin)
    {
        DungeonRoom droom = rndRoom.GetComponent<DungeonRoom>();
        foreach (Connection roomConnection in droom.Connections)
        {
            if (roomConnection == origin) continue;
            DungeonRoom adjacent = GetRoomAt(GetAdjacentRoomPos(wantedPos, roomConnection));
            if (adjacent && !ConnectsTo(adjacent, InvertConnection(roomConnection)))
            {
                return false;
            }
        }
        return true;
    }

    private static bool ConnectsTo(DungeonRoom adjacent, Connection conn)
    {
        foreach (Connection adjacentConnection in adjacent.Connections)
        {
            if (adjacentConnection == conn)
            {
                return true;
            }
        }

        return false;
    }

    private DungeonRoom GetRoomAt(Vector3 newPos)
    {
        foreach (DungeonRoom dungeonRoom in DungeonRooms)
        {
            if (dungeonRoom.transform.position == newPos)
            {
                return dungeonRoom;
            }
        }

        return null;
    }

    private static Vector3 GetAdjacentRoomPos(Vector3 pos, Connection direction)
    {
        switch (direction)
        {
            case Down:
                pos.y -= YOFFSET;
                break;
            case Left:
                pos.x -= XOFFSET;
                break;
            case Up:
                pos.y += YOFFSET;
                break;
            case Right:
                pos.x += XOFFSET;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        return pos;
    }

    private GameObject[] GetRoomPrefabs(Connection direction, bool endRoom)
    {
        switch (direction)
        {
            case Down:
                return endRoom ? UpRoomPrefabs : new[] {EndRoomsPrefabs[(int) Down]};
            case Left:
                return endRoom ? RightRoomPrefabs : new[] {EndRoomsPrefabs[(int) Left]};
            case Up:
                return endRoom ? DownRoomPrefabs : new[] {EndRoomsPrefabs[(int) Up]};
            case Right:
                return endRoom ? LeftRoomPrefabs : new[] {EndRoomsPrefabs[(int) Right]};
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private Connection InvertConnection(Connection _connection)
    {
        switch (_connection)
        {
            case Down:
                return Up;
            case Left:
                return Right;
            case Up:
                return Down;
            case Right:
                return Left;
            case None:
                return None;
            default:
                return None;
        }
    }
}