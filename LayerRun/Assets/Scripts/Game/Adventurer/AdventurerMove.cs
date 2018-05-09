using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerMove : MonoBehaviour
{
    Vector3 m_stPointPosition;
    Vector3 m_stPointCenter;
    Vector3 m_stPosition;
    Vector3 m_stCenterPosition;

    delegate void MoveFunctionType();
    delegate Vector3 InputPositionType(int nIndex);
    MoveFunctionType MoveFunction;
    InputPositionType GetPosition;

    int m_nTouchCount;
    public bool m_bPointState;

    // Use this for initialization
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            MoveFunction = new MoveFunctionType(DesktopMove);
            GetPosition = new InputPositionType(GetMousePosition);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            //MoveFunction = new MoveFunctionType(DesktopMove);
            //GetPosition = new InputPositionType(GetMousePosition);
            MoveFunction = new MoveFunctionType(HandheldMove);
            GetPosition = new InputPositionType(GetTouchPosition);
        }

        m_nTouchCount = 0;
        m_bPointState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateMNG.I.m_eGameState != GAMESTATE.E_PLAY) m_bPointState = false;
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PLAY)
        {
            MoveFunction();
            if (m_bPointState == true)
            {
                m_stPointPosition = GetPosition(m_nTouchCount);
                m_stPointPosition.x -= m_stPointCenter.x;
                m_stPointPosition.y -= m_stPointCenter.y;

                m_stPosition.x = m_stCenterPosition.x + m_stPointPosition.x;
                m_stPosition.y = m_stCenterPosition.y + m_stPointPosition.y;

                if (Mathf.Abs(m_stPosition.x) < 2.8f && m_stPosition.y > -4.5f && m_stPosition.y < 2.75f)
                    transform.position = m_stPosition;
                else
                    SetCenter();
            }
        }
    }

    void DesktopMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_stPointCenter = GetPosition(m_nTouchCount);
            m_stCenterPosition = transform.position;
            m_bPointState = true;
        }
        if (Input.GetMouseButtonUp(0))
            m_bPointState = false;
    }
    void HandheldMove()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            m_nTouchCount = 0;
            m_stPointCenter = GetPosition(m_nTouchCount);
            m_stCenterPosition = transform.position;
            m_bPointState = true;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
            m_bPointState = false;
    }

    void SetCenter()
    {
        m_stPointCenter = GetPosition(m_nTouchCount);
        m_stCenterPosition = transform.position;
    }

    Vector3 GetMousePosition(int nIndex)
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    Vector3 GetTouchPosition(int nIndex)
    {
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(nIndex).position);
    }
}
