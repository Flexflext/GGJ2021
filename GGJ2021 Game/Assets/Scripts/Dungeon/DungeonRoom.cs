using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public enum Connection { Down, Left, Up, Right, None }
    public Connection Origin;
    public Connection[] Connections;
    public List<Connection> OpenConnections= new List<Connection>();
    public bool IsStart = false;
    // public int ConnectionPoints;

    private void Awake()
    {
        for (int i = 0; i < Connections.Length; i++)
        {
            OpenConnections.Add(Connections[i]);
        }
    }
}
