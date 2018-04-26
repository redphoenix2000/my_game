using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_manag : MonoBehaviour {

    private string _gameversion="0.1";
    public GameObject playerprefab;
	// Use this for initialization
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
        Debug.Log("on tente une connexion a une room aleatoire");
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("can't join a Room donc on en créer une");
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
        Debug.Log("On rejoint une room");
        PhotonNetwork.Instantiate("Prefab/" + playerprefab.name, playerprefab.transform.position, Quaternion.identity, 0);
    }
}
