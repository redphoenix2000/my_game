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
                DestroyImmediate(music[0]);
                music[0] = this.gameObject;
                music[1] = null;
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
}
