using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audioscript : MonoBehaviour {

    void Awake()
    {
        
        GameObject []music = GameObject.FindGameObjectsWithTag("music");
        if (music.GetLength(0) > 1 && music[0].name!=music[1].name)
        {
            DestroyImmediate(music[0]);
        }
        else if (music.GetLength(0) > 1 && music[0].name == music[1].name)
        {
            DestroyImmediate(music[1]);
        }
        else
        {
            DontDestroyOnLoad(music[0]);
        }
        
    }
}
