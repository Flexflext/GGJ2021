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
    private List<DungeonRoom> DungeonRooms = new List<DungeonRoom>();

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
        DungeonRooms.Add(startroom);
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
        return CreateRoom(_prefab, _pos, _origin, false);
    }
    private DungeonRoom CreateRoom(GameObject _prefab, Vector3 _pos, DungeonRoom.Connection _origin, bool _isstart)
    {
        GameObject room;
        DungeonRoom droom;
        room = Instantiate(_prefab, _pos, Quaternion.identity, ParentTransform);
        droom = room.GetComponent<DungeonRoom>();
        droom.IsStart = _isstart;
        droom.Origin = _origin;
        DungeonRooms.Add(droom);
        RoomCount += droom.Connections.Length;

        return droom;
    }

    private void AddRoom(DungeonRoom _thisroom)
    {
        if (_thisroom == null)
            return;
        for (int i = 0; i < _thisroom.OpenConnections.Count; i++)
        {
            int rnd = 0;
            Vector3 newpos = _thisroom.transform.position;
            switch (_thisroom.OpenConnections[i])
            {
                case DungeonRoom.Connection.Down:
                    if (_thisroom.Origin != _thisroom.OpenConnections[i])
                    {
                        newpos.y -= YOFFSET;
                        if (RoomCount <= MinimalRoomAmount)
                        {
                            DungeonRoom newroom = null;
                            while (!CheckNextRooms(CreateRoom(UpRoomPrefabs[rnd], newpos, DungeonRoom.Connection.Up), newpos))
                            {
                                rnd = Random.Range(0, UpRoomPrefabs.Length);
                                newroom = CreateRoom(UpRoomPrefabs[rnd], newpos, DungeonRoom.Connection.Up);
                            }
                            OpenRooms.Add(newroom);
                        }
                        else
                        {
                            CreateRoom(EndRoomsPrefabs[0], newpos, DungeonRoom.Connection.Up);
                        }
                    }
                    break;
                case DungeonRoom.Connection.Left:
                    if (_thisroom.Origin == _thisroom.Connections[i])
                        break;
                    newpos.x -= XOFFSET;
                    if (RoomCount <= MinimalRoomAmount)
                    {
                        DungeonRoom newroom = null;
                        while (!CheckNextRooms(newroom, newpos))
                        {
                            rnd = Random.Range(0, RightRoomPrefabs.Length);
                            newroom = CreateRoom(RightRoomPrefabs[rnd], newpos, DungeonRoom.Connection.Right);
                        }
                        OpenRooms.Add(newroom);
                    }
                    else
                    {
                        CreateRoom(EndRoomsPrefabs[1], newpos, DungeonRoom.Connection.Right);
                    }
                    break;
                case DungeonRoom.Connection.Up:
                    if (_thisroom.Origin == _thisroom.Connections[i])
                        break;
                    newpos.y += YOFFSET;
                    if (RoomCount <= MinimalRoomAmount)
                    {
                        DungeonRoom newroom = null;
                        while (!CheckNextRooms(newroom, newpos))
                        {
                            rnd = Random.Range(0, DownRoomPrefabs.Length);
                            newroom = CreateRoom(DownRoomPrefabs[rnd], newpos, DungeonRoom.Connection.Down);
                        }
                        OpenRooms.Add(newroom);
                    }
                    else
                    {
                        CreateRoom(EndRoomsPrefabs[2], newpos, DungeonRoom.Connection.Down);
                    }
                    break;
                case DungeonRoom.Connection.Right:
                    if (_thisroom.Origin == _thisroom.Connections[i])
                        break;
                    newpos.x += XOFFSET;
                    if (RoomCount <= MinimalRoomAmount)
                    {
                        DungeonRoom newroom = null;
                        while (!CheckNextRooms(newroom, newpos))
                        {
                            rnd = Random.Range(0, LeftRoomPrefabs.Length);
                            newroom = CreateRoom(LeftRoomPrefabs[rnd], newpos, DungeonRoom.Connection.Left);
                        }
                        OpenRooms.Add(newroom);
                    }
                    else
                    {
                        CreateRoom(EndRoomsPrefabs[3], newpos, DungeonRoom.Connection.Left);
                    }
                    break;
                default:
                    break;
            }

        }
        OpenRooms.Remove(_thisroom);
    }

    private bool CheckNextRooms(DungeonRoom _thisroom, Vector3 _pos)
    {
        bool result = false;
        //Vector3 nextpos = _pos;
        if (_thisroom == null)
        {
            return false;
        }

        for (int i = 0; i < _thisroom.Connections.Length; i++)
        {
            Vector3 nextpos = _pos;
            if (_thisroom.Connections[i] == DungeonRoom.Connection.Down)
            {
                nextpos.y -= YOFFSET;
                if (_thisroom.Origin != _thisroom.Connections[i])
                {
                    for (int y = 0; y < DungeonRooms.Count; y++)
                    {
                        if (nextpos == DungeonRooms[y].transform.position)
                        {
                            for (int z = 0; z < DungeonRooms[y].Connections.Length; z++)
                            {
                                if (DungeonRooms[y].Connections[z] != InvertConnection(_thisroom.Connections[i]))
                                {
                                    result = false;
                                }
                                else
                                {
                                    DungeonRooms[y].OpenConnections.Remove(DungeonRooms[y].Connections[z]);
                                    if (DungeonRooms[y].OpenConnections.Count == 0)
                                    {
                                        OpenRooms.Remove(DungeonRooms[y]);
                                    }
                                }

                            }
                        }
                        else
                            result = true;
                    }
                }
            }
            else if (_thisroom.Connections[i] == DungeonRoom.Connection.Left)
            {
                nextpos.x -= XOFFSET;
                if (_thisroom.Origin != _thisroom.Connections[i])
                {
                    for (int y = 0; y < DungeonRooms.Count; y++)
                    {
                        if (nextpos == DungeonRooms[y].transform.position)
                        {
                            for (int z = 0; z < DungeonRooms[y].Connections.Length; z++)
                            {
                                if (DungeonRooms[y].Connections[z] != InvertConnection(_thisroom.Connections[i]))
                                {
                                    result = false;
                                }
                                else
                                {
                                    DungeonRooms[y].OpenConnections.Remove(DungeonRooms[y].Connections[z]);
                                    if (DungeonRooms[y].OpenConnections.Count == 0)
                                    {
                                        OpenRooms.Remove(DungeonRooms[y]);
                                    }
                                }
                            }
                        }
                        else
                            result = true;
                    }
                }
            }
            else if (_thisroom.Connections[i] == DungeonRoom.Connection.Up)
            {
                nextpos.y += YOFFSET;
                if (_thisroom.Origin != _thisroom.Connections[i])
                {
                    for (int y = 0; y < DungeonRooms.Count; y++)
                    {
                        if (nextpos == DungeonRooms[y].transform.position)
                        {
                            for (int z = 0; z < DungeonRooms[y].Connections.Length; z++)
                            {
                                if (DungeonRooms[y].Connections[z] != InvertConnection(_thisroom.Connections[i]))
                                {
                                    result = false;
                                }
                                else
                                {
                                    DungeonRooms[y].OpenConnections.Remove(DungeonRooms[y].Connections[z]);
                                    if (DungeonRooms[y].OpenConnections.Count == 0)
                                    {
                                        OpenRooms.Remove(DungeonRooms[y]);
                                    }
                                }
                            }
                        }
                        else
                            result = true;
                    }
                }
            }
            else if (_thisroom.Connections[i] == DungeonRoom.Connection.Right)
            {
                nextpos.x += YOFFSET;
                if (_thisroom.Origin != _thisroom.Connections[i])
                {
                    for (int y = 0; y < DungeonRooms.Count; y++)
                    {
                        if (nextpos == DungeonRooms[y].transform.position)
                        {
                            for (int z = 0; z < DungeonRooms[y].Connections.Length; z++)
                            {
                                if (DungeonRooms[y].Connections[z] != InvertConnection(_thisroom.Connections[i]))
                                {
                                    result = false;
                                }
                                else
                                {
                                    DungeonRooms[y].OpenConnections.Remove(DungeonRooms[y].Connections[z]);
                                    if (DungeonRooms[y].OpenConnections.Count == 0)
                                    {
                                        OpenRooms.Remove(DungeonRooms[y]);
                                    }
                                }
                            }
                        }
                        else
                            result = true;
                    }
                }
            }
        }

        return result;
    }

    private DungeonRoom.Connection InvertConnection(DungeonRoom.Connection _connection)
    {
        switch (_connection)
        {
            case DungeonRoom.Connection.Down:
                return DungeonRoom.Connection.Up;
            case DungeonRoom.Connection.Left:
                return DungeonRoom.Connection.Right;
            case DungeonRoom.Connection.Up:
                return DungeonRoom.Connection.Down;
            case DungeonRoom.Connection.Right:
                return DungeonRoom.Connection.Left;
            case DungeonRoom.Connection.None:
                return DungeonRoom.Connection.None;
            default:
                return DungeonRoom.Connection.None;
        }
    }
}
