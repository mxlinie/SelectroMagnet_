using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueMove : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            anim.SetTrigger("Attack");
    }
}
