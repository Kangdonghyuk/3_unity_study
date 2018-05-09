using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMESTATE
{
    E_NONE = 0,
    E_READY,
    E_PLAY,
    E_PAUSE,
    E_OVER
}

public class GameStateMNG : MonoBehaviour {
    private static GameStateMNG m_Instance = null;
    public static GameStateMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(GameStateMNG)) as GameStateMNG;
                if (m_Instance == null)
                {
                    print("Fail to get GameStateMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    public GAMESTATE m_eGameState;

    // Use this for initialization
    void Start () {
        m_eGameState = GAMESTATE.E_PLAY;
        GamePlay();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetGameState(GAMESTATE.E_PAUSE);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetGameState(GAMESTATE.E_PLAY);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale = 0.5f;
	}

    public void SetGameState(GAMESTATE eGameState)
    {
        m_eGameState = eGameState;
        if (m_eGameState == GAMESTATE.E_PLAY) GamePlay();
        if (m_eGameState == GAMESTATE.E_PAUSE) GamePause();
    }

    public void GamePlay()
    {
        Time.timeScale = 1f;
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
    }
}
