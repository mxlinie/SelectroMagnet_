using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int hitPlayerDamage = 1;
    public GameObject trapTrigger;

    public GameObject trapHitEffect; //Particle Effect for hitting a trap

    [SerializeField] private AudioClip hitPlayer;

    //public GroundDetection groundedScript;

    private void OnTriggerEnter(Collider hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            AudioManager.Instance.phSFX(hitPlayer); //when player enters the trigger zone sound clip plays
            Instantiate(trapHitEffect, transform.position, Quaternion.identity); //When player hits trap particle system with play
            player.PlayerTakeDamage(hitPlayerDamage);
            //groundedScript.grounded = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //groundedScript = GetComponent<GroundDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
