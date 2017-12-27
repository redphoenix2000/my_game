using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;



public class connect : MonoBehaviour
{
    public InputField login;
    public InputField pass;
    MySqlConnection con;
    public Text TextStategeneral;

    public void Connect()
    {

        


    } 
    public void Register()
    {
        SceneManager.LoadScene("Registration");
    }
}
