using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject lightOn;
    public GameObject lightOff;
    public Player player;

    [SerializeField] private AudioClip passCheckpoint;

    void Start()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        lightOn.SetActive(true);
        lightOff.SetActive(false);
        AudioManager.Instance.cSFX(passCheckpoint);
        Debug.Log("check");
    }

}
