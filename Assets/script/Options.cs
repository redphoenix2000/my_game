using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Slider musicvolume;
    public AudioSource my_music;
    public void back()
    {
        SceneManager.LoadScene("Menu");
    }
    void Update()
    {
        AudioListener.volume = musicvolume.value;
    }

}
