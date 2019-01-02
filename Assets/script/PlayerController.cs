using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    private Transform transfo;
    public float speed;
    private int decolage;
    void Start()
    {
        transfo = this.gameObject.GetComponent<Transform>();
        decolage = 0;
        transfo.position = new Vector3(0.006f, 1.051f, 50.675f);
    }

    // Update is called once per frame
    void Update()
    {
        if (decolage > 0)
        {
            transfo.position += new Vector3(0, 0.2f, 0);
            decolage -= 1;
        }
        transfo.position += new Vector3(0, 0, -0.1f);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transfo.position += new Vector3(0.4f, 0, 0) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transfo.position += new Vector3(-0.4f, 0, 0) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transfo.position.y == 1f || transfo.position.y == 2f)
            {
                decolage = 9;
                //transfo.position += new Vector3(0, 1f, 0);
            }
        }
    }
}
