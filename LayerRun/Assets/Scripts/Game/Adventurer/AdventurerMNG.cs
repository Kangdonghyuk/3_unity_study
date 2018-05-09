using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerMNG : MonoBehaviour {
    private static AdventurerMNG m_Instance = null;
    public static AdventurerMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(AdventurerMNG)) as AdventurerMNG;
                if (m_Instance == null)
                {
                    print("Fail to get AdventurerMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    AdventurerLife m_scLife;
    AdventurerGun m_scGun;
    AdventurerMove m_scMove;

    // Use this for initialization
    void Start () {
        m_scLife = GetComponent<AdventurerLife>();
        m_scGun = GetComponent<AdventurerGun>();
        m_scMove = GetComponent<AdventurerMove>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void ChangeGun(GUN_TYPE eGunType)
    {
        m_scGun.ChangeGun(eGunType);
    }
    public void GunInit(GUN_TYPE eGunType)
    {
        m_scGun.GunInit(eGunType);
    }
}
