using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour {

    public float moveSpeed;
    public float rotSpeed;
    public CharacterController pl;
    public NetworkView net;
    public Animator anim;
    public float grav;
    public float jump;
    public Camera cam;
    public int lockaxis = 0;
    private bool isShowing;

    // Use this for initialization
    void Start () {
        pl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        moveSpeed = 6f;
        rotSpeed = 90;
        jump = 8.0f;
        pl.enabled = true;
        pl.transform.position = new Vector3(175,55,132);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {

        //CHECK FOR PLAYER
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
        //ESCAPE 
        if (Input.GetKeyUp("escape"))
        {
            isShowing = !isShowing;
            if (isShowing)
            {
                pl.enabled = false;
                rotSpeed = 0;
                Cursor.visible = true;
            }
            else
            {
                pl.enabled = true;
                rotSpeed = 90;
                Cursor.visible = false;
            }
        }

        var fwd = Input.GetAxis("Vertical");
        var rot = Input.GetAxis("Horizontal");

            if (fwd != 0 || rot != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            //JUMP
            if (pl.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    grav += 5.0f;
                    pl.Move((transform.up *jump *grav) * Time.deltaTime);
                    anim.SetBool("isJumping", true);
                }
                if (Input.GetKey("w")){ pl.Move(transform.forward * Time.deltaTime * moveSpeed); }

                if (Input.GetKey("s")){ pl.Move(-transform.forward * Time.deltaTime * moveSpeed); }

                if (Input.GetKey("a")){ pl.Move(-transform.right * Time.deltaTime * moveSpeed); }

                if (Input.GetKey("d")){  pl.Move(transform.right * Time.deltaTime * moveSpeed); }

                if (grav <= 0)
                    {
                        grav = 0;
                    }
                    else
                    {
                        grav -= 0.2f;
                    }
                
            }
            else
            {
                anim.SetBool("isJumping", false);
                grav -= 0.4f;
                moveSpeed = 3f;
            if (Input.GetKey("w")) { pl.Move(transform.forward * Time.deltaTime * moveSpeed); }

            if (Input.GetKey("s")) { pl.Move(-transform.forward * Time.deltaTime * moveSpeed); }

            if (Input.GetKey("a")) { pl.Move(-transform.right * Time.deltaTime * moveSpeed); }

            if (Input.GetKey("d")) { pl.Move(transform.right * Time.deltaTime * moveSpeed); }
        }
            
            pl.Move(transform.up * grav * Time.deltaTime);




        



        
        rotation();

}
    public override void OnStartLocalPlayer()
    {

    }

    public void rotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed, 0);
    }
}
