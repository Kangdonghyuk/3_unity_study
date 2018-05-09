using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GUN_TYPE
{
    E_BASE = 0,
    E_CIRCLE = 1,
    E_MANY
}

public class AdventurerGun : MonoBehaviour {
    GameObject m_cGun;

    GUN_TYPE m_eGunType;
    GUN_TYPE m_eGunInitType;

    private void Awake()
    {
        m_cGun = transform.Find("Gun").gameObject;
    }

    // Use this for initialization
    void Start () {
        m_eGunInitType = GUN_TYPE.E_BASE;
        m_eGunType = GUN_TYPE.E_BASE;
        m_cGun.AddComponent<BaseGun>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ChangeGun(GUN_TYPE eGunType)
    {
        if (m_eGunType != eGunType)
        {
            DropGun();
            m_eGunType = eGunType;
            SetGun();
        }
    }
    public void GunInit(GUN_TYPE eGunType)
    {
        if(m_eGunType == eGunType)
            ChangeGun(m_eGunInitType);
    }
    void SetGun()
    {
        if (m_eGunType == GUN_TYPE.E_BASE)
            m_cGun.AddComponent<BaseGun>();
        else if (m_eGunType == GUN_TYPE.E_CIRCLE)
            m_cGun.AddComponent<CircleGun>();
        else if (m_eGunType == GUN_TYPE.E_MANY)
            m_cGun.AddComponent<ManyGun>();
    }

    void DropGun()
    {
        if(m_eGunType == GUN_TYPE.E_BASE)
            Destroy(m_cGun.GetComponent<BaseGun>());
        else if(m_eGunType == GUN_TYPE.E_CIRCLE)
            Destroy(m_cGun.GetComponent<CircleGun>());
        else if(m_eGunType == GUN_TYPE.E_MANY)
            Destroy(m_cGun.GetComponent<ManyGun>());
    }
}
