using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    

    #region Variables
    //private Rigidbody myRigidbody;

    public GameObject player; //Set up for player setActive to become false instead of destroying the object


    //Ground Detection & Jumping
    //public GroundDetection groundedScript;
    //public GameObject feet;
    //public int health = 3;

    public float runSpeed;
    public float jumpSpeed;

    public Transform feet;
    public int rayCount;
    public Vector3 rayOffset;

    private bool grounded;
    private float legLength;
    private Rigidbody rb;
    private float horizontal;


    public GameObject losePanel;

    //private float acceleration = 2f;

    //public float speed = 100f;

    //[SerializeField]
    //private float jumpSpeed = 250f;

    private float hitSpeed = 20f;

    private float maxSpeed;

    public int vineDamage = 1; //Just for vines the player bounces off

    //private Rigidbody rb;

    [SerializeField] private AudioClip BounceVineHitplayer;

    //Handling jump movements
    private bool jump;

    //[SerializeField]
    //private Transform[] groundPoints;

    //[SerializeField]
    //private float groundRadius;

    //[SerializeField]
    //private LayerMask whatIsGround; //Adding tags for what is ground

    //[SerializeField]
    //private bool isGrounded; //Checking if the player is in the air

    public GameObject vineHitEffect; //Vine Particle Effect
    public ParticleSystem walkEffect; //Walking particle effect

    // Heart Health System
    public int numOfHearts;
    public int health;

    public Image[] hearts;
    public Sprite filledHeart;
    public Sprite emptyHeart;

    //public Collider mCol;

    //Player respawn
    [SerializeField] public Vector3 respawnPoint;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        numOfHearts = GameManager.Instance.maxHealth;
        health = GameManager.Instance.maxPlayerHealth;
        //jump = false;
        rb = GetComponent<Rigidbody>(); // stating rigibody
        //groundedScript = feet.GetComponent<GroundDetection>();
        legLength = transform.position.y - feet.position.y;
        respawnPoint = this.gameObject.transform.position;
        //mCol = GetComponent<Collider>();

    }

    void Update()
    {
        CheckGround();
        GetInput();
        HeartSystem();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal, rb.velocity.y, 0);
    }

    void CheckGround()
    {
        int layerMask = 1 << 9;
        Vector3 rayPoint = transform.position - ((rayCount / 2) * rayOffset);
        int gCount = 0;
        var em = walkEffect.emission;

        for (int i = 0; i < rayCount; i++)
        {
            Ray ray = new Ray(rayPoint, -transform.up);
            if (Physics.Raycast(ray, legLength, layerMask))
            {
                gCount++;
            }
            Debug.DrawRay(rayPoint, -transform.up * legLength, Color.red);
            rayPoint += rayOffset;
        }
        if (gCount > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
            em.enabled = false; //Character walk particle doesn't play when not grounded
        }

        #region Simple 1 Ray
        // ~~~~~~~ Simple 1 ray check ~~~~~~~~~~
        //Ray ray = new Ray(transform.position, -transform.up);
        //if (Physics.Raycast(ray, legLength, layerMask)) { grounded = true; }
        //else { grounded = false; }
        //Debug.DrawRay(transform.position, -transform.up* legLength, Color.red);
        #endregion
    }

    void GetInput()
    {
        //Debug.Log("Hey");
        if (grounded)
        {
            //Debug.Log("Im grounded");
            horizontal = Input.GetAxis("Horizontal") * runSpeed;
            var em = walkEffect.emission;
            if (horizontal == 0)
            {
                em.enabled = false;
            }
            else if (Mathf.Abs(horizontal) > 0.1f) //Abs is you want to turn a negative value into a positive, doesn't effect other variables
            {
                em.enabled = true;
            }
            //Instantiate(walkEffect, transform.position, Quaternion.identity);
        }
        else
        {
            horizontal = Mathf.Lerp(rb.velocity.x, Input.GetAxis("Horizontal") * runSpeed, Time.deltaTime * 17.5f); //make horizontal movement less responsive when jumping/falling

        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //Debug.Log("Hit Key");
            rb.velocity = new Vector3(rb.velocity.x, 0, 0); //if we land on the edge of a platform sometimes we'll slip off.
            rb.AddForce(0, 100 * jumpSpeed, 0);

        }

        if (horizontal < -0.1f)
        {
            Flip(true); //if moving left with A key, player will flip -180 degrees
            //Instantiate(walkEffect);
        }
        else if (horizontal > 0.1f)
        {
            Flip(false); //Player remains in same position
            //Instantiate(walkEffect, player.transform.position, player.transform.rotation);
        }
    }

    //#region Movement
    //private void HandleMovement(float horizontal) //All horizontal movement through vector 2
    //{
    //    myRigidbody.velocity = new Vector2(horizontal * maxSpeed, myRigidbody.velocity.y);
    //    if (horizontal < -0.1f)
    //    {
    //        Flip(true); //if moving left with A key, player will flip -180 degrees
    //    }
    //    else if (horizontal > 0.1f)
    //    {
    //        Flip(false); //Player remains in same position
    //    }

    //    if (jump && isGrounded)
    //    {
    //        //isGrounded = false;
    //        myRigidbody.AddForce(new Vector2(0, jumpSpeed));
    //        jump = false; // stops player from jumping repeatively
    //        isGrounded = false;
    //    }

    //    else//(!jump && !isGrounded)
    //    {
    //        //jump = true;
    //        //isGrounded = true;
    //        //myRigidbody.AddForce(new Vector2(0, jumpSpeed * 0));

    //    }
    //}

    private void Flip(bool facingRight)
    {
        //facingRight = !facingRight;
        //transform.eulerAngles = new Vector3(0, -180, 0);

        if (facingRight)
        {
            facingRight = false;
            transform.eulerAngles = new Vector3(0, -270, 0);
        }
        else
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }

    // Update is called once per frame

   //IEnumerator MaxSpeed()
   //     {
   //         yield return new WaitForSeconds (0);
   //         maxSpeed = 10f;
        
   //     }

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
        HeartSystem();
        //feet.SetActive(false);
        //groundedScript.grounded = false;
        this.gameObject.transform.position = respawnPoint;
        losePanel.SetActive(true);
        //Destroy(gameObject);
        player.SetActive(false); //Camera error Code doesn't appear anymore
        health = 0;
        GameManager.Instance.health = 3;
    }
    #endregion

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

    //private void HandleInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        jump = true;
    //        //Debug.Log("Key Hit");
    //    }
    //}

    //private void ResetValues()
    //{
    //    jump = false; //Reset jump while in the action
    //}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Vines")
        {
            //Debug.Log("hit");
            //print("ouchie");
            rb.AddForce(Vector3.up * hitSpeed);
            Instantiate(vineHitEffect, transform.position, Quaternion.identity);
            AudioManager.Instance.phthSFX(BounceVineHitplayer); //Plays when player makes contact with vine
            PlayerTakeDamage(vineDamage);
        }
    }


    public void RespawnPlayer()
    {
        //Destroy(GetComponent<Collider>());
        Destroy(feet.GetComponent<Collider>());
        //!mCol.enabled;
        Debug.Log("Hey world!");
        //groundedScript.groundNumber = 0;
        //groundedScript.groundNumber--;
        //groundedScript.grounded = false;
        this.gameObject.transform.position = respawnPoint;
        //groundedScript.groundNumber = 0;
        //groundedScript.grounded = false;
        //jump = false;
        losePanel.SetActive(false);
        health = 3; //Calling public variables directly to change to 3 health with 3 hearts
        numOfHearts = 3;
        HeartSystem(); //Allows the public variables above to work by calling the HeartSystem fuction
        StartCoroutine(ResetCollider());
    }

    IEnumerator ResetCollider()
    {
        yield return new WaitForSeconds(0);
        //feet.SetActive(true);
        //feet.GetComponent<Collider>().enabled = true;
        //feet.AddComponent<BoxCollider>();
        //feet.GetComponent<BoxCollider>().size = new Vector3(0.34f, 0.22f, 1f);
        //feet.GetComponent<BoxCollider>().center = new Vector3(0f, 0.15f, 0f);
        //feet.GetComponent<BoxCollider>().isTrigger = true;
    }

    

}
