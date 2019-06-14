using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    //public int vinePlayerDamage = 1;
    //public GameObject Vine;
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

   /*[SerializeField] private AudioClip hitPlayer;

    private void OnTriggerEnter(Collider hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            AudioManager.Instance.phSFX(hitPlayer); //when player enters the trigger zone sound clip plays
            player.PlayerTakeDamage(vinePlayerDamage);
            Destroy(Vine);
        }

    }*/

}
