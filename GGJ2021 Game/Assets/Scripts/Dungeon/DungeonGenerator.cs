using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DungeonRoom;
using static DungeonRoom.Connection;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    private static readonly Connection[] AllConnections =
        Enum.GetValues(typeof(Connection)).Cast<Connection>().ToArray();

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
        foreach (Transform child in ParentTransform)
        {
            Destroy(child.gameObject);
        }

        DungeonRooms.Clear();
        OpenRooms.Clear();

        DungeonRoom startRoom = CreateRoom(Up, DungeonStartPos);
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

    private DungeonRoom CreateRoom(Connection direction, Vector3 originPos)
    {
        Connection origin = InvertConnection(direction);
        Vector3 newPos = GetAdjacentRoomPos(originPos, direction);
        bool endRoom = DungeonRooms.Count >= MinimalRoomAmount;
        GameObject[] roomPrefabs = GetRoomPrefabs(origin, endRoom);

        var existingRoom = GetRoomAt(newPos);
        if (existingRoom)
        {
            // A room already exists here, so we don't even have to start searching for a fitting one
            // Maybe check connections and throw error if something doesn't add up?
            return null;
        }


        GameObject fittingRoom = FindFittingRoom(roomPrefabs, newPos, origin);
        if (fittingRoom == null)
        {
            roomPrefabs = GetRoomPrefabs(origin, !endRoom);
            fittingRoom = FindFittingRoom(roomPrefabs, newPos, origin);
            if (fittingRoom == null)
            {
                Debug.LogError(
                    $"failed to find room after for ({origin}, {roomPrefabs.Length}, {newPos})"
                );
                return null;
            }
        }

        GameObject room = Instantiate(fittingRoom, newPos, Quaternion.identity, ParentTransform);
        DungeonRoom droom = room.GetComponent<DungeonRoom>();
        droom.Origin = origin;

        DungeonRooms.Add(droom);
        OpenRooms.Add(droom);

        return droom;
    }

    private GameObject FindFittingRoom(IEnumerable<GameObject> roomPrefabs, Vector3 newPos, Connection origin)
    {
        foreach (GameObject roomPrefab in roomPrefabs)
        {
            if (DoesRoomFit(newPos, roomPrefab, origin))
            {
                return roomPrefab;
            }
        }

        return null;
    }

    private bool DoesRoomFit(Vector3 wantedPos, GameObject rndRoom, Connection origin)
    {
        DungeonRoom droom = rndRoom.GetComponent<DungeonRoom>();

        foreach (Connection connection in AllConnections)
        {
            if (connection == origin) continue;
            DungeonRoom adjacent = GetRoomAt(GetAdjacentRoomPos(wantedPos, connection));
            if (adjacent)
            {
                bool weConnect = ConnectsTo(droom, connection);
                bool heConnects = ConnectsTo(adjacent, InvertConnection(connection));

                if (!weConnect && !heConnects || weConnect && heConnects)
                {
                    // Entweder es klickt, oder es klickt halt nicht
                    continue;
                }

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
        GameObject[] prefabs;
        switch (direction)
        {
            case Down:
                prefabs = !endRoom ? DownRoomPrefabs : new[] {EndRoomsPrefabs[(int) Down]};
                break;
            case Left:
                prefabs = !endRoom ? LeftRoomPrefabs : new[] {EndRoomsPrefabs[(int) Left]};
                break;
            case Up:
                prefabs = !endRoom ? UpRoomPrefabs : new[] {EndRoomsPrefabs[(int) Up]};
                break;
            case Right:
                prefabs = !endRoom ? RightRoomPrefabs : new[] {EndRoomsPrefabs[(int) Right]};
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        return prefabs.OrderBy(x => Random.Range(0F, 10F)).ToArray();
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
            default:
                throw new ArgumentOutOfRangeException(nameof(_connection), _connection, null);
        }
    }
}