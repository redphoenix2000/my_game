using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
public class player_info : MonoBehaviour {

    public Text mail;
    public Text pseudotext;
    public string host;
    public string database;
    public string username;
    public string password;
    MySqlConnection con;
    // Use this for initialization
    void Awake ()
    {
        try
        {
            ConnectBdd();
            if (GameObject.FindGameObjectWithTag("player") != null)
            {
                GameObject pseudo = GameObject.FindGameObjectWithTag("player");
                pseudotext.text = "Pseudo : " + pseudo.name;
                MySqlCommand command_info = new MySqlCommand("SELECT * FROM users WHERE pseudo = '" + @pseudo.name + "'", con);
                MySqlDataReader my_Reader = command_info.ExecuteReader();
                while (my_Reader.Read())
                {                  
                    mail.text = "Email : " + my_Reader.GetString(3);
                }
                my_Reader.Close();
                con.Close();
            }
        }
        catch (IOException ex)
        {
            mail.text = "Error :" + ex;
    
        }
        
    }
	
    public void ConnectBdd()
    {
        string constr = "Server=" + host + ";DATABASE=" + database + ";User ID=" + username + ";Password=" + DataBaseManag.crypt256(password) + ";Pooling=true;Charset=utf8;";
        try
        {
            con = new MySqlConnection(constr);
            con.Open();
            
        }
        catch (IOException ex)
        {
            Debug.Log(ex.ToString());
        }

    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
