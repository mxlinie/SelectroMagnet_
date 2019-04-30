using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T>:MonoBehaviour where T : MonoBehaviour
{
    private static T instance_;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
