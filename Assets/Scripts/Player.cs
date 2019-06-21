using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    private Rigidbody myRigidbody;

    public GameObject player; //Set up for player setActive to become false instead of destroying the object

    public GroundDetection groundedScript;
    public GameObject feet;
    //public int health = 3;

    public GameObject losePanel;

    private float acceleration = 2f;

    public float speed = 100f;

    [SerializeField]
    private float jumpSpeed = 250f;

    private float hitSpeed = 20f;

    private float maxSpeed;

    public int vineDamage = 1; //Just for vines the player bounces off

    private Rigidbody rb;

    //Handling jump movements
    private bool jump;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround; //Adding tags for what is ground

    [SerializeField]
    private bool isGrounded; //Checking if the player is in the air

    // Heart Health System
    public int numOfHearts;
    public int health;

    public Image[] hearts;
    public Sprite filledHeart;
    public Sprite emptyHeart;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); // stating rigibody
        groundedScript = feet.GetComponent<GroundDetection>();
    }

    #region Movement
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

        if (jump && isGrounded)
        {
            //isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpSpeed));
            jump = false; // stops player from jumping repeatively
        }

        else//(!jump && !isGrounded)
        {
            //jump = true;
            isGrounded = true;
            myRigidbody.AddForce(new Vector2(0, jumpSpeed * 0));
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
        isGrounded = groundedScript.grounded;

        HandleInput(); //Holds Space bar jumping
        //isGrounded = IsGrounded();
        float horizontal = Input.GetAxis("Horizontal"); // horizontal movement no stated so HandleMovement will function
        HandleMovement(horizontal); //Calling function
        //ControlMovement();
        Vector3 movement = new Vector3(acceleration, 0, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        //transform.rotation = Quaternion.LookRotation(movement);
        //if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);\


        /*if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddForce(Vector3.up * jumpSpeed);
            //transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime, Space.World);
        }*/

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

        HeartSystem(); //Running HeartSystem Function
               
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
    #endregion

    #region PlayerTakeDamage
    public void PlayerTakeDamage(int hitPlayerDamage) //when a player collides with a trap
    {
        health -= hitPlayerDamage; //minus health
        GameManager.Instance.HealthScore(hitPlayerDamage); //Call HealthScore function in GameManager 
        if (health <= 0)
        {
            PlayerDie();
        }

    }
    #endregion

    #region PlayerHealthGrab
    public void PlayerHealthGrab(int playerHealthHit) //When a player collides with a health pack
    {
        health += playerHealthHit; //plus health
        GameManager.Instance.HealthScorePack(playerHealthHit); //Call HealthScore function in GameManager 
    }
    #endregion

    #region Death
    void PlayerDie()
    {
        losePanel.SetActive(true);
        //Destroy(gameObject);
        player.SetActive(false); //Camera error Code doesn't appear anymore
        GameManager.Instance.health = 3;
    }
    #endregion

    /*void ControlMovement()
    {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.rotation = Quaternion.LookRotation(movement);


        transform.Translate(movement * maxSpeed * Time.deltaTime, Space.World);
    }*/

    public void HeartSystem()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = filledHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            //Debug.Log("Key Hit");
        }
    }

    private void ResetValues()
    {
        jump = false; //Reset jump while in the action
    }

    /*private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    print("Weh");
                    if (colliders[i].gameObject != gameObject)
                    {
                        Debug.Log(colliders[i].gameObject.name);
                        print("double weh");
                        return true;
                    }
                }
            }
        }
        return false;
    }*/

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Vines")
        {
            //Debug.Log("hit");
            print("ouchie");
            myRigidbody.AddForce(Vector3.up * hitSpeed);
           
            PlayerTakeDamage(vineDamage);
        }
    }

}
