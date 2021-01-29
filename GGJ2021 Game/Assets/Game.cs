using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField]
    private UIManager m_UIManager;
    [SerializeField]
    private GameObject m_Player;
    private PlayerBuffScript m_PlayerBuff;

    public UIManager UIManager
    {
        get { return m_UIManager; }
    }
    public PlayerBuffScript Playerbuff { get { return m_PlayerBuff; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        m_PlayerBuff = m_Player.GetComponent<PlayerBuffScript>();

    }
}
