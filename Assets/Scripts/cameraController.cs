using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public float rotSpeed;
    public bool isShowing;
    // Use this for initialization
    void Start () {
        rotSpeed = 90;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp("escape"))
        {
            isShowing = !isShowing;
            if (isShowing)
            {
                rotSpeed = 0;
            }
            else
            {
                rotSpeed = 90;
            }
            
        }
        transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed, 0, 0);
    }
}
