using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGun : MonoBehaviour
{

    //private Rigidbody rb;
    private Rigidbody myRigidbody;

    private float maxSpeed;
    private float acceleration = 2f;

    public float speed = 100f;

    public int fR;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); // stating rigibody
    }

    void FixedUpdate() //gun rotation
    {
        Vector3 mousePos = Input.mousePosition;
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
        //Vector3 movement = new Vector3(acceleration, 0, 0);
    }

    private void HandleMovement(float horizontal) //All horizontal movement through vector 2
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

    }

    public void Flip(bool facingRight)
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
    }

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
