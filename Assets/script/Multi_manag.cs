using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Multi_manag : MonoBehaviour {

    private string _gameversion="0.1";
    public GameObject playerprefab;
    public Dropdown list_room;
    public InputField create_room;
    public Text status;
    private GameObject panel_conf;

    void Awake()
    {
        panel_conf = GameObject.Find("room_panel");
    }
    void Start ()
    {
        PhotonNetwork.ConnectUsingSettings(_gameversion);
	}

    // Update is called once per frame
    void Update ()
    {
        
	}
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
    void OnReceivedRoomListUpdate()
    {
        list_room.ClearOptions();
        list_room.options.Add(new Dropdown.OptionData("Rooms"));
       foreach(RoomInfo room in PhotonNetwork.GetRoomList())
        {
            list_room.options.Add(new Dropdown.OptionData(room.Name));
        }
    }
    void chooseRoom()
    {
        RoomInfo[] inforoom = PhotonNetwork.GetRoomList();
        if(create_room.text!=""  && list_room.value.ToString() == "0")
        {
            bool create = true;
            foreach (RoomInfo room in inforoom)
            {
                if (create_room.text==room.Name)
                {
                    create= false;
                }
            }
            if (create==true)
            {
                PhotonNetwork.CreateRoom(create_room.text);
                Debug.Log("Room créer");
            }
            else if(create==false)
            {
                status.text = "Status : Room already exist";
                chooseRoom();
            }
        }
        else if (create_room.text == "" && list_room.value.ToString() != "0")
        {
            PhotonNetwork.JoinRoom(list_room.options[list_room.value].text);
        }
        else
        {
            status.text = "Status : An error occured";
        }
        
    }

    void OnJoinedRoom()
    {
        Debug.Log("On rejoint la room");
        PhotonNetwork.Instantiate("Prefab/" + playerprefab.name, playerprefab.transform.position, Quaternion.identity, 0);
    }

    void OnPhotonCreateRoomFailed(object [] codeAndMsg)
    {
        status.text = "Status : Can't Join that Room" + codeAndMsg[1];
    }
    public void go()
    {
        if (create_room.text != "" && list_room.value.ToString()=="0")
        {
            chooseRoom();
            panel_conf.gameObject.SetActive(false);
        }
        else if(create_room.text=="" && list_room.value.ToString()!="0")
        {
            chooseRoom();
            panel_conf.gameObject.SetActive(false);
        }
        else
        {
            status.text = "Status : You have to create a Room";
        }
    }
    public void Menu()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }
    public void randomRoomJoin()
    {
        if (PhotonNetwork.GetRoomList().Length>0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            status.text = "Status : We can't join a random room";
        }
    }

}
