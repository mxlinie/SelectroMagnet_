using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        //transform.LookAt(target);

        Vector3 targetPostition = new Vector3(target.position.x,this.transform.position.y,target.position.z);
        this.transform.LookAt(target);
    }
}
