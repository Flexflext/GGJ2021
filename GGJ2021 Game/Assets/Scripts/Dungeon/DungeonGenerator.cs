using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int RoomCount = 0;

    private List<DungeonRoom> OpenRooms = new List<DungeonRoom>();

    private void Start()
    {
        GenerateDungeon();
    }

    [ContextMenu("GenerateDungeon")]
    public void GenerateDungeon()
    {
        int rnd = Random.Range(0, DownRoomPrefabs.Length);
        DungeonRoom startroom = CreateRoom(DownRoomPrefabs[rnd], DungeonStartPos, DungeonRoom.Connection.Down, true);
        OpenRooms.Add(startroom);
        startroom.IsStart = true;

        while (OpenRooms.Count != 0)
        {
            for (int i = 0; i < OpenRooms.Count; i++)
            {
                AddRoom(OpenRooms[i]);
            }
        }
    }

    private DungeonRoom CreateRoom(GameObject _prefab, Vector3 _pos, DungeonRoom.Connection _origin)
    {
        //GameObject room;
        //room = Instantiate(_prefab, _pos, Quaternion.identity, ParentTransform);

        //return room.GetComponent<DungeonRoom>();
        return CreateRoom(_prefab, _pos, _origin, false);
    }
    private DungeonRoom CreateRoom(GameObject _prefab, Vector3 _pos, DungeonRoom.Connection _origin, bool _isstart)
    {
        GameObject room;
        DungeonRoom droom;
        room = Instantiate(_prefab, _pos, Quaternion.identity, ParentTransform);
        droom = room.GetComponent<DungeonRoom>();
        droom.IsStart = _isstart;
        droom.Origin = DungeonRoom.Connection.Down;
        RoomCount += droom.Connections.Length;

        return droom;
    }

    private void AddRoom(DungeonRoom _thisroom)
    {
        for (int i = 0; i < _thisroom.Connections.Length; i++)
        {
            int rnd;
            Vector3 newpos = _thisroom.gameObject.transform.position;
            switch (_thisroom.Connections[i])
            {
                case DungeonRoom.Connection.Down:
                    if (_thisroom.Origin != _thisroom.Connections[i])
                    {
                        newpos.y -= YOFFSET;
                        if (RoomCount <= MinimalRoomAmount)
                        {
                            rnd = Random.Range(0, UpRoomPrefabs.Length);
                            OpenRooms.Add(CreateRoom(UpRoomPrefabs[rnd], newpos, _thisroom.Origin));
                        }
                        else
                        {
                            CreateRoom(EndRoomsPrefabs[0], newpos, _thisroom.Origin);
                        }
                    }
                    break;
                case DungeonRoom.Connection.Left:
                    if (_thisroom.Origin == _thisroom.Connections[i])
                        break;
                    newpos.x -= XOFFSET;
                    if (RoomCount <= MinimalRoomAmount)
                    {
                        rnd = Random.Range(0, RightRoomPrefabs.Length);
                        OpenRooms.Add(CreateRoom(RightRoomPrefabs[rnd], newpos, _thisroom.Origin));
                    }
                    else
                    {
                        CreateRoom(EndRoomsPrefabs[1], newpos, _thisroom.Origin);
                    }
                    break;
                case DungeonRoom.Connection.Up:
                    if (_thisroom.Origin == _thisroom.Connections[i])
                        break;
                    newpos.y += YOFFSET;
                    if (RoomCount <= MinimalRoomAmount)
                    {
                        rnd = Random.Range(0, DownRoomPrefabs.Length);
                        OpenRooms.Add(CreateRoom(DownRoomPrefabs[rnd], newpos, _thisroom.Origin));
                    }
                    else
                    {
                        CreateRoom(EndRoomsPrefabs[2], newpos, _thisroom.Origin);
                    }
                    break;
                case DungeonRoom.Connection.Right:
                    if (_thisroom.Origin == _thisroom.Connections[i])
                        break;
                    newpos.x += XOFFSET;
                    if (RoomCount <= MinimalRoomAmount)
                    {
                        rnd = Random.Range(0, LeftRoomPrefabs.Length);
                        OpenRooms.Add(CreateRoom(LeftRoomPrefabs[rnd], newpos, _thisroom.Origin));
                    }
                    else
                    {
                        CreateRoom(EndRoomsPrefabs[3], newpos, _thisroom.Origin);
                    }
                    break;
                default:
                    break;
            }

        }
        OpenRooms.Remove(_thisroom);
    }
}
