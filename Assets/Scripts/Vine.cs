using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{

    public int vinePlayerDamage = 1;
    public GameObject vine;
    [SerializeField] private AudioClip VhitPlayer;

    private void OnTriggerEnter(Collider hitInfo)
 {
     Player player = hitInfo.GetComponent<Player>();
     if (player != null)
     {
         AudioManager.Instance.phSFX(VhitPlayer); //when player enters the trigger zone and gets hit sound clip plays
         player.PlayerTakeDamage(vinePlayerDamage);
            vine.SetActive(false);
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
