using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public bool fullscreen;
    public float musicvolume;
    public int quality;
    public int resolution;
    public void back()
    {
        SceneManager.LoadScene("Menu");
    }

}
