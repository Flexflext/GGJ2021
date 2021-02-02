using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Unit
{
    protected override void Initialize()
    {
        base.Initialize();
        m_StartPosition = transform.position;
    }
}
