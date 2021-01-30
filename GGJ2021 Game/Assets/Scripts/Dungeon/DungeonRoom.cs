using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public enum Connection { Down, Left, Up, Right, None }
    public Connection Origin;
    public Connection[] Connections;
    public bool IsStart = false;
    // public int ConnectionPoints;
}
