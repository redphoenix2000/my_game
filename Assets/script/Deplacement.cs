using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour {

    float speed = 200f;
    PhotonView view;
	void Start ()
    {
        view = GetComponent<PhotonView>();
	}
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0.0f, v);
        if(h!=0f || v!=0f)
        {
            if (view.isMine)
            {
                GetComponent<Rigidbody>().velocity = movement * speed * Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
