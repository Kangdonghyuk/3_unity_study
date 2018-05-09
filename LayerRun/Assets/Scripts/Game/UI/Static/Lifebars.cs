using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebars : MonoBehaviour {

    GameObject m_cLifebarPrefab;
    GameObject m_cCreateLifebar;
    SpriteRenderer[] m_scLifebarSprite;

    int m_nLife;
    float m_fDamage;
    float m_fLifebarNumber;
    int m_nLifebarMaxNumber = 40;

	// Use this for initialization
	void Awake () {
        m_cLifebarPrefab = (GameObject)Resources.Load("Prefabs/Game/UI/Static/Lifebar");
	}

    private void Start()
    {
        m_nLifebarMaxNumber = 40;
        m_scLifebarSprite = new SpriteRenderer[m_nLifebarMaxNumber];
        for(int nIndex = 0; nIndex < m_nLifebarMaxNumber; nIndex++)
        {
            m_cCreateLifebar = Instantiate(m_cLifebarPrefab);
            m_cCreateLifebar.transform.parent = transform;
            m_cCreateLifebar.transform.position = new Vector3(-2.847f + (0.146f * nIndex),
                transform.position.y, 0f);
            m_scLifebarSprite[nIndex] = m_cCreateLifebar.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Down(1);
    }

    public void Set(int nLife)
    {
        m_nLife = nLife;
        m_fDamage = ((float)m_nLifebarMaxNumber / (float)m_nLife);
        m_fLifebarNumber = 0;
    }
	
    public void Down(int nDamage)
    {
        m_nLife -= nDamage;
        if (m_nLife > 0)
        {
            for (int nIndex = 0; nIndex < nDamage; nIndex++)
                m_fLifebarNumber += m_fDamage;
            for (int nIndex = 0; nIndex < m_fLifebarNumber; nIndex++)
                m_scLifebarSprite[m_nLifebarMaxNumber - nIndex - 1].enabled = false;
        }
        else
            for (int nIndex = 0; nIndex < m_nLifebarMaxNumber; nIndex++)
                m_scLifebarSprite[nIndex].enabled = false;
    }
}
