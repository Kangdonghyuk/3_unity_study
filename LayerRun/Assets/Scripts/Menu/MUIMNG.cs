using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUIMNG : MonoBehaviour
{
    private static MUIMNG m_Instance = null;
    public static MUIMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(MUIMNG)) as MUIMNG;
                if (m_Instance == null)
                {
                    print("Fail to get MUIMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    //public GameObject m_cPausePopup;

    private void Awake()
    {
        //m_cPausePopup = transform.Find("Popup").Find("PausePopup").gameObject;
    }

    private void Start()
    {
        //m_cPausePopup.SetActive(false);
    }

    public void ClickBtn(MBTNKIND eBtnKind)
    {
        if (eBtnKind == MBTNKIND.E_PLAY) Play();
        else if (eBtnKind == MBTNKIND.E_EXIT) Exit();
    }

    void Play()
    {
        SceneMNG.I.LoadScene(SCENEKIND.E_GAME);
    }
    void Exit()
    {
        Application.Quit();
    }
}
