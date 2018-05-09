using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour {

    public int m_nDamage;

    float m_fSpeed;
    float m_fNowTime;
    float m_fLifeTime;

	// Use this for initialization
	void Start () {
        m_fSpeed = 10;
        m_nDamage = 1;
        m_fNowTime = Time.deltaTime;
        m_fLifeTime = m_fNowTime + 2f;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PLAY)
        {
            m_fNowTime += Time.deltaTime;
            if (m_fNowTime >= m_fLifeTime || Mathf.Abs(transform.position.y) >= 5f
                || Mathf.Abs(transform.position.x) >= 9.7f)
                Destroy(gameObject);
            transform.Translate(Vector3.up * Time.deltaTime * m_fSpeed);
        }
	}

    public void Create(Vector3 stPosition, Quaternion stRotation, Vector3 stScale,
                int nDamage, float fSpeed, string sTag)
    {
        transform.position = stPosition;
        transform.rotation = stRotation;
        transform.localScale = stScale;
        m_nDamage = nDamage;
        m_fSpeed = fSpeed;
        transform.tag = sTag;
    }
}
