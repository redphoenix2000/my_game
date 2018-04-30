using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps_camera : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    GameObject character;
    PhotonView view;
    void Start()
    {
        character = this.transform.parent.gameObject;
        view = GetComponent<PhotonView>();
    }


    // Update is called once per frame
    void Update ()
    {
   
            var md = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity - smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
        if (view.isMine)
        {
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
	}
}
