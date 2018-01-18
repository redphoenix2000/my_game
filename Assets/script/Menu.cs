using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Text TextStategeneral;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            GameObject welcome = GameObject.FindGameObjectWithTag("player");
            TextStategeneral.text = "Welcome " + welcome.name;
        }
    }
    public void exit()
    {
        Application.Quit();
    }
    public void options()
    {
        SceneManager.LoadScene("Options");
    }



}
