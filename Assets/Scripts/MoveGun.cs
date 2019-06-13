using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGun : MonoBehaviour
{

    //private Rigidbody rb;
    private Rigidbody myRigidbody;

    private float maxSpeed;
    private float acceleration = 2f;
    public GameObject Sgun; //SelectroMagnet Gun
    public bool FacingRight = true;

    public float Horizontal;
    public float MS = 7.0f;

    public float speed = 100f;



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); // stating rigibody
    }

    void FixedUpdate() //gun rotation
    {
        /*Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

       Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
       mousePos.x = mousePos.x - objectPos.x;
       mousePos.y = mousePos.y - objectPos.y;

       float angleY = Mathf.Clamp(mousePos.y, -60, 60);
       float angleX = Mathf.Clamp(mousePos.x, -180, 0);
       transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleY));

        //GunRotation();

        float horizontal = Input.GetAxis("Horizontal"); // horizontal movement no stated so HandleMovement will function
        HandleMovement(horizontal); //Calling function
        //Vector3 movement = new Vector3(acceleration, 0, 0);*/
        // I put the aiming biz into a separate event to keep the Update/Fixed Update section clean.
        AimGun();

        // Binding the camera to the character
        //Cam.gameObject.transform.position = this.gameObject.transform.position + CamOffset;


        // This is what I did for simple left-right movement. 
        Horizontal = Input.GetAxis("Horizontal") * MS;
        myRigidbody.velocity = new Vector3(Horizontal, 0, 0);
        // The "&& FacingRight" means it will only change the bool when we are changing direction
        // It won't try to do anything if we press right twice
        if (Input.GetAxis("Horizontal") > 0f && !FacingRight)
        {
            FacingRight = true;
            this.transform.Rotate(0, -180f, 0);
        }
        if (Input.GetAxis("Horizontal") < 0f && FacingRight)
        {
            FacingRight = false;
            this.transform.Rotate(0, 180f, 0);
        }
    }

    /*private void HandleMovement(float horizontal) //All horizontal movement through vector 2
    {
        myRigidbody.velocity = new Vector2(horizontal * maxSpeed, myRigidbody.velocity.y);
        if (horizontal < -0.1f)
        {
            Flip(true); //if moving left with A key, player will flip -180 degrees
        }
        else if (horizontal > 0.1f)
        {
            Flip(false); //Player remains in same position
        }

    }*/

    void AimGun()
    {
        // Pretty sure the code here isn't any different to yours.
        Vector3 mousePos = Input.mousePosition;

        // Because my PlayerController binds the camera, I use my CamOffset.z to allow for variable distances.
        mousePos.z = 5.23f;

        Vector3 gunPos = Camera.main.WorldToScreenPoint(Sgun.transform.position);
        mousePos.x = mousePos.x - gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;
        float angleY = Mathf.Clamp(mousePos.y, -60, 60);
        float angleX = Mathf.Clamp(mousePos.x, -180, 0);

        // --- THE ACTUAL FIX ---
        // This is the "Magic" that fixes your several layers of flipping hell
        // The code is pretty much identical, just set the 180 degree rotation pending a FacingRight bool check.
        if (FacingRight)
        {
            Sgun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleY));
        }
        else if (!FacingRight)
        {
            Sgun.transform.rotation = Quaternion.Euler(new Vector3(0, 180, angleY));
        }

    }

    /*public void Flip(bool facingRight)
    {
        //facingRight = !facingRight;
        //transform.eulerAngles = new Vector3(0, -180, 0);

        if (facingRight)
        {
            facingRight = false;
            //fR = -1;
            transform.eulerAngles = new Vector3(0, -180, 0);

        }
        else
        {
            facingRight = true;
            //fR = 0;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }*/
    
    /*public void GunRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angleY = Mathf.Clamp(mousePos.y, -60, 60);
        float angleX = Mathf.Clamp(mousePos.x, -180, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleY));
    }*/

}
