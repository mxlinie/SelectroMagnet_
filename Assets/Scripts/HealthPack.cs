using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int playerHealthHit = 1;

    [SerializeField] private AudioClip healthPickUp;

    public GameObject healthPickUpEffect; //Particle Effect

    private void OnTriggerEnter(Collider hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            AudioManager.Instance.phthSFX(healthPickUp); //when player hits health pack AudioManager script will make the audio source and play sound
            player.PlayerHealthGrab(playerHealthHit);
            Instantiate(healthPickUpEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); //health pack destroyed when hit
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
