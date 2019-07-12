using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{


    public GameObject lightOn;
    public GameObject lightOff;
    public GameObject PlayerR;

    [SerializeField] private AudioClip passCheckpoint;
    private Player ps;

    void Start()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
        ps = PlayerR.GetComponent<Player>();
        //ps.RespawnPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        lightOn.SetActive(true);
        lightOff.SetActive(false);
        AudioManager.Instance.cSFX(passCheckpoint);
        ps.respawnPoint = this.gameObject.transform.position;
        //Debug.Log("check");
    }

}
