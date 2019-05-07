using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private AudioSource leftMouse; //AudioSource will be created for left mouse click
    private AudioSource rightMouse;
    private AudioSource checkpointHit;
    private AudioSource playerHit;
    private AudioSource playerHealth;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        leftMouse = this.gameObject.AddComponent<AudioSource>(); //AudioSource added to scene
        rightMouse = this.gameObject.AddComponent<AudioSource>();
        checkpointHit = this.gameObject.AddComponent<AudioSource>();
        playerHit = this.gameObject.AddComponent<AudioSource>();
        playerHealth = this.gameObject.AddComponent<AudioSource>();
    }

    public void lmSFX(AudioClip clip) //left mouse click
    {
        leftMouse.PlayOneShot(clip);
    }

    public void rmSFX(AudioClip clip) //right mouse click
    {
        rightMouse.PlayOneShot(clip);
    }

    public void cSFX(AudioClip clip) //checkpoint passed
    {
        checkpointHit.PlayOneShot(clip);
    }

    public void phSFX(AudioClip clip) //player hit by trap
    {
        playerHit.PlayOneShot(clip);
    }

    public void phthSFX(AudioClip clip) //player hit
    {
        playerHealth.PlayOneShot(clip);
    }
}
