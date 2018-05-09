using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerLife : MonoBehaviour {

    Lifebars m_scLifebars;

    int m_nLife;

    private void Awake()
    {
        m_scLifebars = GameObject.Find("AdventurerLifebars").GetComponent<Lifebars>();
    }

    // Use this for initialization
    void Start () {
        m_nLife = 20;
        m_scLifebars.Set(m_nLife);
    }
	
    public void SetLife(int nLife)
    {
        m_nLife = nLife;
        m_scLifebars.Set(m_nLife);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            m_nLife--;
            Destroy(collision.gameObject);
            m_scLifebars.Down(1);
            if (m_nLife <= 0)
                Destroy(gameObject);
        }
    }
}
