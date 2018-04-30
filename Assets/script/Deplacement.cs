using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour{

    private float speed;
    private float jumpForce;
    public GameObject cam;
    bool view;
	void Start ()
    {
        speed = 7f;
        jumpForce = 9f;
        if(view)
        {
            cam.gameObject.SetActive(true);
        }
        else { cam.gameObject.SetActive(false); }

    }
    void Awake()
    {
        view = GetComponent<PhotonView>().isMine;
    }
    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update ()
    { 
                    float h = Input.GetAxis("Horizontal");
                    float v = Input.GetAxis("Vertical");
                    if (h != 0f || v != 0f)
                    { 
                    if (view)
                    { 
                        transform.Translate(speed * h * Time.deltaTime, 0f, speed * v * Time.deltaTime);
                        if (Input.GetKey(KeyCode.Space))
                        {
                            Debug.Log("Space");
                            GetComponent<Rigidbody>().velocity = new Vector3(0f, jumpForce);
                        }
                    }
                }  
	}
}
