using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(Screen.height/16*10, Screen.height, true);
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
