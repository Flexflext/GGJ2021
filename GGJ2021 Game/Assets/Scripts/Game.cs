using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField]
    private UIManager m_UIManager;

    public GameObject Player;
    
    public UIManager UIManager
    {
        get { return m_UIManager; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }
}
