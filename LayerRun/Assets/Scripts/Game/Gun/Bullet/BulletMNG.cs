using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMNG : MonoBehaviour {
    private static BulletMNG m_Instance = null;
    public static BulletMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(BulletMNG)) as BulletMNG;
                if (m_Instance == null)
                {
                    print("Fail to get BulletMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void AllRemoveBullet()
    {
        int nChildCount = transform.childCount;
        for (int nIndex = 0; nIndex < nChildCount; nIndex++)
            Destroy(transform.GetChild(nIndex).gameObject);
    }
}
