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
    public Text grade;
    public Text error;
    public Text pseudotext;
    public string host;
    public string database;
    public string username;
    public string password;
    MySqlConnection con;
    private enum grades {Troufion,Commandant,Colonnel,General};
    private GameObject panel;
    public InputField lastpass;
    public InputField new_pass;
    public InputField confirm_pass;
    private GameObject pseudo;
    private GameObject pass;
    private GameObject grad;
    private GameObject email;
    private GameObject menu_button;
    private GameObject update_button;

    // Use this for initialization
    void Awake ()
    {
        pseudo = GameObject.Find("player_pseudo");
        pass = GameObject.Find("player_password");
        email = GameObject.Find("player_email");
        grad = GameObject.Find("player_grade");
        menu_button = GameObject.Find("Menu");
        update_button = GameObject.Find("button_update");
        panel = GameObject.Find("menu_update");
        panel.gameObject.SetActive(false);
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
                    grade.text = "Grade : " + ((grades) my_Reader.GetInt16(4)).ToString();
                    
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
    public void update_pass()
    {
        pseudo.gameObject.SetActive(false);
        pass.gameObject.SetActive(false);
        email.gameObject.SetActive(false);
        grad.gameObject.SetActive(false);
        menu_button.gameObject.SetActive(false);
        update_button.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
    }
    public void apply()
    {
        try
        {
            ConnectBdd();
            bool b = false;
            GameObject pseudo = GameObject.FindGameObjectWithTag("player");
            MySqlCommand command_info = new MySqlCommand("SELECT * FROM users WHERE pseudo = '" + @pseudo.name + "'", con);
            MySqlDataReader my_Reader = command_info.ExecuteReader();
            while (my_Reader.Read())
            {
                if (DataBaseManag.crypt256(lastpass.text) == my_Reader.GetString(2) && new_pass.text==confirm_pass.text)
                {
                    b = true;
                }
                if(lastpass.text=="" || new_pass.text=="" || confirm_pass.text=="")
                {
                    b = false;
                    error.text = "Error : " + "Select all input";
                }
                else
                {
                    error.text = "Error : " + "Maybe wrong late pass or pass different";
                }

            }
            my_Reader.Close();
            if (b)
            {
                string command = "UPDATE users SET password= '" + @DataBaseManag.crypt256(new_pass.text) + "' WHERE pseudo='" + pseudo.name + "';";
                MySqlCommand cmd = new MySqlCommand(command, con);
                try
                {
                    cmd.ExecuteReader();
                    Debug.Log("Update success");
                }
                catch (IOException ex)
                {
                    Debug.Log(ex);
                }
                cmd.Dispose();
                panel.gameObject.SetActive(false);
                pseudo.gameObject.SetActive(true);
                pass.gameObject.SetActive(true);
                email.gameObject.SetActive(true);
                grad.gameObject.SetActive(true);
                menu_button.gameObject.SetActive(true);
                update_button.gameObject.SetActive(true);
            }
            con.Close();
        }
        catch (IOException ex)
        {
            Debug.Log(ex);
        }
    }
    public void menu_quit()
    {
        pseudo.gameObject.SetActive(true);
        pass.gameObject.SetActive(true);
        email.gameObject.SetActive(true);
        grad.gameObject.SetActive(true);
        menu_button.gameObject.SetActive(true);
        update_button.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
    }
}
