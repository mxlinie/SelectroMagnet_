using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    /*
    public Transform player; //This creates a slot in the inspector where you can add your player
    public float maxDistance = 5f; //This can be changed in the inspector to your liking
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < maxDistance)
        { //transform is the object that this script is attached to
            rigidbody.useGravity = true;
        }
    }
    */

    public Rigidbody FallingObject;
    //public int hitPlayerDamage = 1;

    //[SerializeField] private AudioClip hitPlayer;

    public void OnTriggerEnter(Collider collider)
    {
        if (FallingObject.isKinematic)
            FallingObject.isKinematic = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Debug.Log("Got you!");
        }
    }

}
