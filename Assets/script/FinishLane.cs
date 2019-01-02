using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			other.gameObject.SetActive(false);
			SceneManager.LoadScene("FinishScene", LoadSceneMode.Additive);
		}
	}
}
