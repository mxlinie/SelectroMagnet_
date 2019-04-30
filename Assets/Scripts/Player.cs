using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody myRigidbody;

    public int health = 3;

    public GameObject losePanel;

    private float acceleration = 2f;

    public float speed = 100f;

    private float jumpSpeed = 10f;

    private float maxSpeed;
       

    private Rigidbody rb;
    // Start is called before the first frame update
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
        }
        else
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal"); // horizontal movement no stated so HandleMovement will function
        HandleMovement(horizontal); //Calling function
        //ControlMovement();
        Vector3 movement = new Vector3(acceleration, 0, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        //transform.rotation = Quaternion.LookRotation(movement);
        //if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);\


        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            
            myRigidbody.AddForce(-movement * speed);
            if (speed >= maxSpeed)
            {
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
                //rb.transform.rotation = 
                MaxSpeed();
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            
            myRigidbody.AddForce(movement * speed);
            if (speed >= maxSpeed)
            {
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
                //rb.velocity = transform.right * maxSpeed;
                MaxSpeed();
            }
        }
               
        //float moveHorizontal = Input.GetAxis("Horizontal");
        ////float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0);
        //rb.AddForce(movement * speed);*/
    }
   IEnumerator MaxSpeed()
        {
            yield return new WaitForSeconds (0);
            maxSpeed = 10f;
        
        }

    public void PlayerTakeDamage(int hitPlayerDamage)
    {
        health -= hitPlayerDamage;
        GameManager.Instance.HealthScore(hitPlayerDamage); //Call HealthScore function in GameManager 
        if (health <= 0)
        {
            PlayerDie();
        }

    }

    void PlayerDie()
    {
        losePanel.SetActive(true);
        Destroy(gameObject);
        GameManager.Instance.health = 3;
    }

    /*void ControlMovement()
    {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.rotation = Quaternion.LookRotation(movement);


        transform.Translate(movement * maxSpeed * Time.deltaTime, Space.World);
    }*/
}
