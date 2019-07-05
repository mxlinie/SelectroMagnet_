using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();

    private Vector3 orgPosPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        orgPosPlatforms = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //platforms = orgPosPlatforms;
        }
    }
}
