using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;



public class connect : MonoBehaviour 
{
    public string host;
    public string database;
    public string username;
    public string password;
    public InputField my_login;
    public InputField my_pass;
    MySqlConnection con;
    public Text TextStategeneral;

    public void ConnectBdd_2()
    {
        string constr = "Server=" + host + ";DATABASE=" + database + ";User ID=" + username + ";Password=" + crypt256(password) + ";Pooling=true;Charset=utf8;";
        try
        {
            con = new MySqlConnection(constr);
            con.Open();
            TextStategeneral.text = con.State.ToString();
        }
        catch (IOException ex)
        {
            TextStategeneral.text = ex.ToString();
        }

    }
    static string crypt256(string pass)
    {
        System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
        System.Text.StringBuilder myhash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(pass), 0, Encoding.UTF8.GetByteCount(pass));
        foreach (byte theByte in crypto)
        {
            myhash.Append(theByte.ToString("x2"));
        }
        return myhash.ToString();
    }

    public void Connect()
    {
        ConnectBdd_2();
        string pass_pseudo = null;
        try
        {
            MySqlCommand command_login = new MySqlCommand("SELECT * FROM users WHERE pseudo = '" + @my_login.text + "'", con);
            MySqlDataReader my_Reader = command_login.ExecuteReader();
            while (my_Reader.Read())
            {
                pass_pseudo = my_Reader["password"].ToString();
                if (pass_pseudo == @crypt256(my_pass.text))
                {
                    GameObject player_info = GameObject.FindGameObjectWithTag("player");
                    player_info.name = my_login.text;
                    TextStategeneral.text = "Welcome " + my_login.text;
                    DontDestroyOnLoad(player_info);
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    TextStategeneral.text = "Wrong password for " + my_login.text;
                }
            }
            if (pass_pseudo == null)
            {
                TextStategeneral.text = my_login.text +" account does not exist.";
            }
            my_Reader.Close();
        }
        catch(IOException exec)
        {
            TextStategeneral.text = exec.ToString();
        }
        con.Close();

        

    } 
    public void Register()
    {
        SceneManager.LoadScene("Registration");
    }

}
