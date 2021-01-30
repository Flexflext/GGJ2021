using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public enum Connection { Down, Left, Up, Right }

    public Connection Origin;
    public Connection[] Connections;
    public bool IsStart = false;
}
