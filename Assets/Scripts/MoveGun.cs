using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGun : MonoBehaviour
{
   
    void Update()
    {
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
