using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Adventurer
{
    Transform m_cTransform;
    GameObject m_cGunObject;
    AdventurerGun m_scGun;
    AdventurerLife m_scLife;
    AdventurerMove m_scMove;
    public void Set(Transform cTransform)
    {
        m_cTransform = cTransform;
        m_cGunObject = m_cTransform.Find("Gun").gameObject;
        m_scGun = m_cTransform.GetComponent<AdventurerGun>();
        m_scLife = m_cTransform.GetComponent<AdventurerLife>();
        m_scMove = m_cTransform.GetComponent<AdventurerMove>();
    }
    public void Play()
    {
        m_cGunObject.SetActive(true);
        m_scGun.enabled = true;
        m_scLife.enabled = true;
        m_scMove.enabled = true;
    }
    public void Pause()
    {
        m_cGunObject.SetActive(false);
        m_scGun.enabled = false;
        m_scLife.enabled = false;
        m_scMove.m_bPointState = false;
        m_scMove.enabled = false;
    }
}

public struct Enemy
{
    Transform m_cTransform;
    GameObject m_cGunObject;
    EnemyLife m_scLife;
    public void Set(Transform cTransform)
    {
        m_cTransform = cTransform;
        m_cGunObject = m_cTransform.Find("Gun").gameObject;
        m_scLife = m_cTransform.GetComponent<EnemyLife>();
    }
    public void Play()
    {
        m_cGunObject.SetActive(true);
        m_scLife.enabled = true;
    }
    public void Pause()
    {
        m_cGunObject.SetActive(false);
        m_scLife.enabled = false;
    }
}

public struct Bullet
{
    Transform m_cTransform;
    GameObject m_cGunObject;
    EnemyLife m_scLife;
    public void Set(Transform cTransform)
    {
        m_cTransform = cTransform;
        m_cGunObject = m_cTransform.Find("Gun").gameObject;
        m_scLife = m_cTransform.GetComponent<EnemyLife>();
    }
    public void Play()
    {
        m_cGunObject.SetActive(true);
        m_scLife.enabled = true;
    }
    public void Pause()
    {
        m_cGunObject.SetActive(false);
        m_scLife.enabled = false;
    }
}

public class GameObjectMNG : MonoBehaviour {
    private static GameObjectMNG m_Instance = null;
    public static GameObjectMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(GameObjectMNG)) as GameObjectMNG;
                if (m_Instance == null)
                {
                    print("Fail to get GameObjectMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    Adventurer m_stAdventurer;
    Enemy m_stEnemy;

    // Use this for initialization
    void Start () {
        m_stAdventurer.Set(GameObject.Find("Adventurer").transform);
        m_stEnemy.Set(GameObject.Find("Enemy_Temp").transform);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Pause();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Play();
    }

    public void Play()
    {
        Time.timeScale = 1;
        //m_stAdventurer.Play();
        //m_stEnemy.Play();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        //m_stAdventurer.Pause();
        //m_stEnemy.Pause();
    }
}
