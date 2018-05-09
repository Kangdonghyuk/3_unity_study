using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    SpriteRenderer m_scSprite;
    Color m_cColor;

    float m_fNowTime;
    float m_fSkillTime;
    float m_fWaitTime;
    public float m_fInputSkillTime;
    public float m_fInputWaitTime;

    bool m_bState;
    bool m_bFinishState;

    RaycastHit2D m_stPick; // Get Raycast Transform
    Transform m_cPickTransform; // Pick Pang Transform
    Vector2 m_stPickPosition; // ScreenToWorld(PickPosition)

    // Use this for initialization
    public void Start()
    {
        m_scSprite = transform.GetComponent<SpriteRenderer>();
        m_cColor = m_scSprite.color;
        m_cColor.a = 0f;
        m_scSprite.color = m_cColor;

        m_fNowTime = Time.deltaTime;

        m_bState = true;
        m_bFinishState = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PLAY)
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
                Handheld();
            else if (SystemInfo.deviceType == DeviceType.Desktop)
                Desktop();

            if (Time.timeScale == 1f)
                m_fNowTime += Time.deltaTime;
            else
                m_fNowTime += (Time.deltaTime * 2f);

            if (m_bState == false)
            {
                if (m_fNowTime >= m_fWaitTime)
                    Can();
                if (m_fNowTime >= m_fSkillTime && m_bFinishState == false)
                {
                    Finish();
                }
            }
        }
    }

    public void Use()
    {
        transform.parent.SendMessage("Use");
        m_cColor.a = 1f;
        m_scSprite.color = m_cColor;

        m_fSkillTime = m_fNowTime + m_fInputSkillTime;
        m_fWaitTime = m_fNowTime + m_fInputWaitTime;
        m_bState = false;
        m_bFinishState = false;
    }
    public void Finish()
    {
        transform.parent.SendMessage("Finish");
        m_bFinishState = true;
    }
    public void Can()
    {
        m_cColor.a = 0f;
        m_scSprite.color = m_cColor;
        m_bState = true;
    }

    void Handheld()
    {
        for(int nIndex = 0; nIndex < Input.touchCount; nIndex++)
        {
            if(Input.GetTouch(nIndex).phase == TouchPhase.Began)
            {
                m_stPickPosition = Camera.main.ScreenToWorldPoint(
                    Input.GetTouch(nIndex).position);
                if (Raycast() == true)
                    if (m_bState == true)
                        Use();
            }
        }
    }

    void Desktop()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            m_stPickPosition = Camera.main.ScreenToWorldPoint(
                    Input.mousePosition);
            if (Raycast() == true)
                if (m_bState == true)
                    Use();
        }
    }

    bool Raycast()
    {
        m_stPick = Physics2D.Raycast(m_stPickPosition, Vector2.zero);
        if (m_stPick.transform != null)
            if (m_stPick.transform == transform.parent)
                return true;
        return false;
    }
}
