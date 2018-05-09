using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENEKIND
{
    E_LOGO = 0,
    E_MENU,
    E_GAME
}

public class SceneMNG : MonoBehaviour {
    private static SceneMNG m_Instance = null;
    public static SceneMNG I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(SceneMNG)) as SceneMNG;
                if (m_Instance == null)
                {
                    print("Fail to get SceneMNG");
                    return null;
                }
            }
            return m_Instance;
        }
    }

    SCENEKIND m_eNowScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(SCENEKIND eSceneKind)
    {
        if (eSceneKind == SCENEKIND.E_GAME)
        {
            SceneManager.LoadScene("MainGame");
        }
        else if (eSceneKind == SCENEKIND.E_MENU)
            SceneManager.LoadScene("MainMenu");
    }
}
