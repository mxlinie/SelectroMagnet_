using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGun : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private float maxSpeed;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); // stating rigibody
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

    private void Flip(bool facingRight)
    {
        //facingRight = !facingRight;
        //transform.eulerAngles = new Vector3(0, -180, 0);

        if (facingRight)
        {
            facingRight = false;
            transform.eulerAngles = new Vector3(0, -180, 0);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angleY = Mathf.Clamp(mousePos.y, -60, 60);
            float angleNY = Mathf.Clamp(mousePos.y, 60, -60);
            float angleX = Mathf.Clamp(mousePos.x, -180, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNY));
        }
        else
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angleY = Mathf.Clamp(mousePos.y, -60, 60);            
            float angleX = Mathf.Clamp(mousePos.x, -180, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleY));
        }
    }
    //void Update()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = 5.23f;

    //    Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
    //    mousePos.x = mousePos.x - objectPos.x;
    //    mousePos.y = mousePos.y - objectPos.y;

    //    float angleY = Mathf.Clamp(mousePos.y, -60, 60);
    //    float angleX = Mathf.Clamp(mousePos.x, -180, 0);
    //    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleY));
    //}

}
