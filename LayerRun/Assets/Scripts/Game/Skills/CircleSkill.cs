using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSkill : MonoBehaviour {
    Skills m_scSkills;

	// Use this for initialization
	void Start () {
        m_scSkills = transform.GetChild(0).GetComponent<Skills>();
        m_scSkills.m_fInputSkillTime = 10f;
        m_scSkills.m_fInputWaitTime = 20f;
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public void Use()
    {
        AdventurerMNG.I.ChangeGun(GUN_TYPE.E_CIRCLE);
    }
    public void Finish()
    {
        AdventurerMNG.I.GunInit(GUN_TYPE.E_CIRCLE);
    }
}
