using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int hitPlayerDamage = 1;
    public GameObject trapTrigger;

    [SerializeField] private AudioClip hitPlayer;

    private void OnTriggerEnter(Collider hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            AudioManager.Instance.phSFX(hitPlayer); //when player enters the trigger zone sound clip plays
            player.PlayerTakeDamage(hitPlayerDamage);
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
