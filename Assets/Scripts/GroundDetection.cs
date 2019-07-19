using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public static GroundDetection Instance;

    public bool grounded;

    public int groundNumber;




    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I am in the trigger");
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            groundNumber++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("I am out of the trigger");
        if (other.gameObject.tag == "Ground")
        {
            //grounded = false;
            groundNumber--;
            if (groundNumber <= 0)
            {
                grounded = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (groundNumber <= 0)
        {
            grounded = false;
        }
    }
}
