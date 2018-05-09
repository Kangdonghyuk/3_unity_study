using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSkill : MonoBehaviour
{
    Skills m_scSkills;

    // Use this for initialization
    void Start()
    {
        m_scSkills = transform.GetChild(0).GetComponent<Skills>();
        m_scSkills.m_fInputSkillTime = 3f;
        m_scSkills.m_fInputWaitTime = 8f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Use()
    {
        Time.timeScale = 0.5f;
    }
    public void Finish()
    {
        Time.timeScale = 1f;
    }
}
