using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] Waypoints;

    public float platformSpeed = 2;

        public int CurrentPoint = 0;
    //public float posY;
    //public float posZ;
    //https://answers.unity.com/questions/827225/moving-platform-help-c.html
    
    void FixedUpdate()
    {
        //new Vector3 platPos(0,posY,posZ); //error when I opened game
        if(transform.position.z != Waypoints[CurrentPoint].transform.position.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, platformSpeed * Time.deltaTime);
        }

        if(transform.position.z == Waypoints[CurrentPoint].transform.position.z)
        {
            CurrentPoint += 1;
        }
        if(CurrentPoint >= Waypoints.Length)
        {
            CurrentPoint = 0;
        }
    }
}
