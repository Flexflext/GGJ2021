using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField]
    private UIManager m_UIManager;
    [SerializeField]
    private PlayerManager m_PlayerManager;

    public PlayerManager PlayerManager => m_PlayerManager;
    
    public UIManager UIManager => m_UIManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(this);
    }
}
