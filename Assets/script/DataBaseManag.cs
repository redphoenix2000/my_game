using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MySql.Data.MySqlClient;
using System.Text;

public class DataBaseManag : MonoBehaviour {


    public string host;
    public string database;
    public string username;
    public string password;
    public Text TextState;
    MySqlConnection con;
    public InputField login;
    public InputField pass;
    public Text TextLogin;

	// Use this for initialization
	void ConnectBdd ()
    {
        string constr = "Server=" + host + ";DATABASE=" + database + ";User ID=" + username + ";Password=" + crypt256(password) + ";Pooling=true;Charset=utf8;";
        try
        {
            con = new MySqlConnection(constr);
            con.Open();
            TextState.text = con.State.ToString();
        }
        catch (IOException ex)
        {
            TextState.text = ex.ToString();
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

    // Update is called once per frame
    void Update ()
    {
		
	}
    private void OnApplicationQuit()
    {
        Debug.Log("Shutdown Connexion");
        if (con != null && con.State.ToString() != "Closed")
        {
            con.Close();
        }
    }
    public void Register()
    {
        bool userex=false;
        ConnectBdd();
        MySqlCommand commver = new MySqlCommand("SELECT pseudo FROM users WHERE pseudo='" + login.text + "'", con);
        MySqlDataReader myreader = commver.ExecuteReader();
        while (myreader.Read())
        {
            if (myreader["pseudo"].ToString() != "")
            {
                TextLogin.text = "Pseudo alreadu exist !!!";
                userex = true;
            }

        }
        myreader.Close();
        if (!userex)
        {
            string regisComm = "INSERT INTO users VALUES (default,'" + login.text + "','" + crypt256(pass.text) + "')";
            MySqlCommand cmd = new MySqlCommand(regisComm, con);
            try
            {
                cmd.ExecuteReader();
                TextLogin.text = "Register sucessfull";

            }
            catch (IOException ex)
            {
                TextLogin.text = ex.ToString();
            }
            cmd.Dispose();
            con.Close();
        }

    }

}
