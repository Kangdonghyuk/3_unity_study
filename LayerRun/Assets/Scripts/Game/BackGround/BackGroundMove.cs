using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRepeat
{
    public SpriteRenderer[] m_scSprite;
    public Transform m_cTransform;
    public Vector3 m_stPosition;
    public BackGroundRepeat(Transform cBGRTransform, Sprite cSprite, int nChildIndex)
    {
        m_scSprite = new SpriteRenderer[2];
        m_cTransform = cBGRTransform;
        m_stPosition = m_cTransform.position;
        for (int nIndex = 0; nIndex < 2; nIndex++)
        {
            m_scSprite[nIndex] = m_cTransform.GetChild(nIndex).GetComponent<SpriteRenderer>();
            m_scSprite[nIndex].sprite = cSprite;
            m_scSprite[nIndex].transform.localPosition = new Vector3(0f, (nIndex * (cSprite.rect.height/100f)), 0f);
        }
        m_stPosition.y = nChildIndex * ((cSprite.rect.height/100f) * 2f);
        m_cTransform.position = m_stPosition;
    }
    public void Down(float fSpeed, float fTime)
    {
        m_stPosition.y -= fSpeed * fTime;
        m_cTransform.position = m_stPosition;
    }
    public void SetPositionY(float fYPosition)
    {
        m_stPosition.y = fYPosition;
        m_cTransform.position = m_stPosition;
    }
}

public class BackGroundMove : MonoBehaviour {
    private static BackGroundMove m_Instance = null;
    public static BackGroundMove I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(BackGroundMove)) as BackGroundMove;
                if (m_Instance == null)
                {
                    print("Fail to get BackGroundMove");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    public Sprite m_cBGSprite;
    BackGroundRepeat[] m_cBGRList;

    Transform m_cBGR;
    Vector3 m_stPosition;

    int m_nChildNumber;
    float m_fSpeed;

    SpriteRenderer m_sc;

    private void Awake()
    {
        m_cBGR = transform.Find("BackGround_Repeat");
        //m_cBGSprite = Resources.Load("Images/Game/UI/Popup/PausePopup", typeof(Sprite)) as Sprite;
        m_cBGSprite = Resources.Load("Images/Game/BackGround/BackGround_Temp", typeof(Sprite)) as Sprite;
    }

    // Use this for initialization
    void Start () {
        m_nChildNumber = m_cBGR.childCount;
        m_cBGRList = new BackGroundRepeat[m_nChildNumber];
        for (int nIndex = 0; nIndex < m_nChildNumber; nIndex++)
            m_cBGRList[nIndex] = new BackGroundRepeat(m_cBGR.GetChild(nIndex), m_cBGSprite, nIndex);

        m_fSpeed = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PLAY)
        {
            BGRDown();
            //BGRMove();
        }
    }

    void BGRMove()
    {
        if(Input.GetKey(KeyCode.A) || Input.acceleration.x < -0.2f)
        {
            if(transform.position.x < 6.35f)
                transform.Translate(Vector3.right * Time.deltaTime * m_fSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.acceleration.x > 0.2f)
        {
            if (transform.position.x > -6.35f)
                transform.Translate(Vector3.left * Time.deltaTime * m_fSpeed);
        }
    }

    void BGRDown()
    {
        for (int childIndex = 0; childIndex < m_nChildNumber; childIndex++)
            m_cBGRList[childIndex].Down(m_fSpeed, Time.deltaTime);
        for (int childIndex = 0; childIndex < m_nChildNumber; childIndex++)
        {
            if (m_cBGRList[childIndex].m_stPosition.y <= -(m_cBGSprite.rect.height / 100f) * 3f)
            {
                m_cBGRList[childIndex].SetPositionY(
                    m_cBGRList[GetNextIndex(childIndex)].m_stPosition.y + (m_cBGSprite.rect.height / 100f) * 2f);
            }
        }
    }

    int GetNextIndex(int nIndex) {
        if (nIndex == 0)    return m_nChildNumber-1;
        return nIndex - 1;
    }
}
