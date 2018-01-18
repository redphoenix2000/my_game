using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audioscript : MonoBehaviour {

    void Awake()
    {
        
        GameObject []music = GameObject.FindGameObjectsWithTag("music");
        if (music.GetLength(0) > 1)
        {
            Scene actual = SceneManager.GetActiveScene();
            if (actual.name == "Menu")
            {
                Debug.Log(actual.name);
                DestroyImmediate(music[0]);
            }
            else
            {
                music[1].name = "second";
                DestroyImmediate(music[1]);
            }            
        }
        else
        {
            music[0].name = "first";
            DontDestroyOnLoad(music[0]);
        }
        
    }
}
