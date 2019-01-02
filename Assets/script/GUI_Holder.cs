using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Holder : MonoBehaviour {

    Text statusText;
    Text masterText;
	void Start ()
    {
        statusText = GameObject.Find("Status").GetComponent<Text>();
        masterText = GameObject.Find("Master").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        statusText.text = "Status : " + PhotonNetwork.connectionStateDetailed.ToString();
        masterText.text = "isMasterClient : " + PhotonNetwork.isMasterClient;
    }
}
