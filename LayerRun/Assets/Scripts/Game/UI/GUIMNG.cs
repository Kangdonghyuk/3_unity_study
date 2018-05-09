using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMNG : MonoBehaviour
{
    private static GUIMNG m_Instance = null;
    public static GUIMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(GUIMNG)) as GUIMNG;
                if (m_Instance == null)
                {
                    print("Fail to get GUIMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    public GameObject m_cPausePopup;

    private void Awake()
    {
        //m_cPausePopup = transform.Find("Popup").Find("PausePopup").gameObject;
    }

    private void Start()
    {
        m_cPausePopup.SetActive(false);
    }

    public void ClickBtn(GBTNKIND eBtnKind)
    {
        if (eBtnKind == GBTNKIND.E_PAUSE) Pause();
        else if (eBtnKind == GBTNKIND.E_PLAY) Play();
        else if (eBtnKind == GBTNKIND.E_AGAIN) Again();
        else if (eBtnKind == GBTNKIND.E_MENU) Menu();
    }

    void Pause()
    {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PLAY)
        {
            m_cPausePopup.SetActive(true);
            GameStateMNG.I.SetGameState(GAMESTATE.E_PAUSE);
        }
    }
    void Play()
    {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PAUSE)
        {
            m_cPausePopup.SetActive(false);
            GameStateMNG.I.SetGameState(GAMESTATE.E_PLAY);
        }
    }
    void Again()
    {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PAUSE)
            SceneMNG.I.LoadScene(SCENEKIND.E_GAME);
    }
    void Menu()
    {
        SceneMNG.I.LoadScene(SCENEKIND.E_MENU);
    }
}
