using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        PlayerRespawn(); //Moved function so it would appear on script
    }

    public void PlayerRespawn()
    {
        //player.transform.position = respawnPoint.transform.position;

    }

}
