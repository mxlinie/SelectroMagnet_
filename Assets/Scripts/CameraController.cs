using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //I want to create a script to have the camera follow my submarine. 
    //Depending on how my game goes I could have the camera move automatically and have the submarine try to keep up.

    public GameObject Player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}
