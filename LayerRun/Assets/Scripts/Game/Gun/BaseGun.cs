using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour {
    GameObject m_cBulletPrefab;
    GameObject m_cCreateBullet;
    BaseBullet m_scBaseBullet;

    Transform m_cBulletParent;
    Transform m_cGun;

    float m_fNowTime;
    float m_fShootTime;
    public float m_fShootSpeed = 0.5f;

    private void Awake()
    {
        m_cBulletPrefab = (GameObject)Resources.Load("Prefabs/Game/Bullet/Bullet_Temp");
        m_cBulletParent = GameObject.Find("Bullet").transform;
    }

    // Use this for initialization
    void Start () {
        m_cGun = transform;

        m_fNowTime = Time.deltaTime;
        m_fShootTime = Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStateMNG.I.m_eGameState == GAMESTATE.E_PLAY)
        {
            m_fNowTime += Time.deltaTime;
            if (m_fNowTime >= m_fShootTime)
            {
                Shoot();
                m_fShootTime = m_fNowTime + m_fShootSpeed;
            }
        }
    }

    void Shoot()
    {
        m_cCreateBullet = Instantiate(m_cBulletPrefab);
        m_cCreateBullet.transform.parent = m_cBulletParent;
        m_scBaseBullet = m_cCreateBullet.GetComponent<BaseBullet>();

        m_scBaseBullet.Create(m_cGun.position, m_cGun.rotation,
            new Vector3(1f, 1.5f),
            1, 10f, transform.tag);
    }
}
