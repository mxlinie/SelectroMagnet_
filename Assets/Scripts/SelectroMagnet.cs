using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SelectroMagnet : MonoBehaviour   

{
    
    private RaycastHit hit;
    

    public GameObject rayCastPoint;
    public GameObject positive;
    public GameObject negative;
    public Animation anim;
    public AnimationClip clip;
   
   

    public void Start()
    {
        anim.AddClip(clip, "MovingFloor");
        anim.Stop();
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            
           Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, 10))
            {
                if (hit.transform.gameObject.tag == ("Positive"))
                    Debug.Log("North");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {

            

            if (Physics.Raycast(transform.position, transform.forward, hitInfo: out hit))
            {
                if (hit.transform.tag == "Negative")
                    Debug.Log("South");
                {
                    anim.Play("MovingFloor");
            
                        }
            }
            //if (anim.isPlaying)
            //{
            //    return;
            //}

        }
        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
