using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MBTNKIND
{
    E_PLAY,
    E_SHOP,
    E_EXIT
}

public class MBtnClick : MonoBehaviour
{
    public MBTNKIND m_eBtnKind;

    RaycastHit2D m_stPick; // Get Raycast Transform
    Transform m_cPickTransform; // Pick Pang Transform
    Vector2 m_stPickPosition; // ScreenToWorld(PickPosition)

    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            Handheld();
        else if (SystemInfo.deviceType == DeviceType.Desktop)
            Desktop();
    }

    void Handheld()
    {
        for (int nIndex = 0; nIndex < Input.touchCount; nIndex++)
        {
            if (Input.GetTouch(nIndex).phase == TouchPhase.Began)
            {
                m_stPickPosition = Camera.main.ScreenToWorldPoint(
                    Input.GetTouch(nIndex).position);
                if (Raycast() == true)
                    MUIMNG.I.ClickBtn(m_eBtnKind);
            }
        }
    }

    void Desktop()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            m_stPickPosition = Camera.main.ScreenToWorldPoint(
                    Input.mousePosition);
            if (Raycast() == true)
                MUIMNG.I.ClickBtn(m_eBtnKind);
        }
    }

    bool Raycast()
    {
        m_stPick = Physics2D.Raycast(m_stPickPosition, Vector2.zero);
        if (m_stPick.transform != null)
            if (m_stPick.transform == transform)
                return true;
        return false;
    }
}
