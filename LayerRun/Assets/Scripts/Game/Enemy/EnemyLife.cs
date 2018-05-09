using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour {

    Lifebars m_scLifebars;

    public int m_nMaxLife = 100;
    int m_nLife;

    private void Awake()
    {
        m_scLifebars = GameObject.Find("EnemyLifebars").GetComponent<Lifebars>();
    }

    // Use this for initialization
    void Start () {
        m_nLife = m_nMaxLife;
        m_scLifebars.Set(m_nMaxLife);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Adventurer")
        {
            m_nLife--;
            Destroy(collision.gameObject);
            m_scLifebars.Down(1);
            if (m_nLife == 0)
            {
                BulletMNG.I.AllRemoveBullet();
                Destroy(gameObject);
            }
        }
    }
}
