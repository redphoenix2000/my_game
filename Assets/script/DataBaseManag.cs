using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Data;
using System.Net.Mail;
using System.Linq;
using System.Threading;
using System.IO;
using MySql.Data.MySqlClient;
using System.Text;
using UnityEngine.SceneManagement;
[ExecuteInEditMode]
public class DataBaseManag : MonoBehaviour {

    public string smtp;
    public string host;
    public string database;
    public string username;
    public string password;
    public Text TextState;
    MySqlConnection con;
    public InputField login;
    public InputField email;
    public InputField pass;
    public Text TextLogin;

    public void ConnectBdd ()
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
    public static string crypt256(string pass)
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
        int port = 25;
        bool userex=false;
        ConnectBdd();
        MySqlCommand commver = new MySqlCommand("SELECT pseudo FROM users WHERE pseudo='" + @login.text + "'", con);
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
        if (login.text.Length > 9 )
        {
            TextLogin.text = "pseudo < 10 char";
            userex = true;
        }
        if (!userex)
        {
            if (login.text == "" || pass.text == "" || email.text == "")
            {
                TextLogin.text = "You should right something into all case !!!";
                userex = true;
            }
        }
        if (!userex)
        {
            try
            {
                SmtpClient my_server = new SmtpClient(smtp, port);
                my_server.EnableSsl = true;
                my_server.UseDefaultCredentials = false;
                my_server.Credentials = new System.Net.NetworkCredential(username + "@alwaysdata.net", crypt256(password));
                my_server.DeliveryMethod = SmtpDeliveryMethod.Network;
                my_server.EnableSsl = false;
                string my_mail = username + "@alwaysdata.net";
                string dest = email.text;
                MailMessage msg = new MailMessage(my_mail, dest);
                msg.Subject = "TheOuCafe";
                msg.Body = "You have been registered as " + login.text;
                my_server.Send(msg);

            }
            catch (Exception my_execpt)
            {
                TextLogin.text = "Maybe Wrong email ";
                Debug.Log(my_execpt);
                userex = true;
            }
        }
        if (!userex)
        {
            string regisComm = "INSERT INTO users VALUES (default,'" + @login.text + "','" + @crypt256(pass.text) +"','"+@email.text+ "','" +"0"+ "')";
            MySqlCommand cmd = new MySqlCommand(regisComm, con);
            try
            {
                cmd.ExecuteReader();
                TextLogin.text = "Register sucessfull";
                SceneManager.LoadScene("Login");

            }
            catch (IOException ex)
            {
                TextLogin.text = ex.ToString();
            }
            cmd.Dispose();
            OnApplicationQuit();
        }

    }
    public void exit()
    {
        OnApplicationQuit();
        Application.Quit();
    } 

}
