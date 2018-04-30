using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Text TextStategeneral;
    private AudioSource music;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            GameObject welcome = GameObject.FindGameObjectWithTag("player");
            TextStategeneral.text = "Welcome " + welcome.name;
        }
    }
    private void Start()
    {
        music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("volume"))
        {
            float vol = PlayerPrefs.GetFloat("volume");
            music.volume = vol;
        }
        if (PlayerPrefs.HasKey("screen"))
        {
            Screen.fullScreen = PlayerPrefs.GetString("screen") == "True";
        }
        if (PlayerPrefs.HasKey("quality"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        }
        if (PlayerPrefs.HasKey("width") && PlayerPrefs.HasKey("height"))
        {
            Resolution[] resolutions = Screen.resolutions;
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen);
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
    public void profil()
    {
        SceneManager.LoadScene("profil");
    }
    public void singleplayer()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void multiplayer()
    {
        SceneManager.LoadScene("multi");
    }



}
