using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cane : MonoBehaviour
{
    public static Cane Instance;

    public string Canemessage; //Set personalised message in inspector

    [SerializeField] private AudioClip objectCanePickUp; //Same as watch

    public GameObject pickupPanel; //Same as watch

    private void Awake()
    {
        Instance = this;
    }

    void OnTriggerStay(Collider other)
    {
        UIManager.Instance.ToggleText(Canemessage); //Message set in the inspector
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) //if player is near watch object can be picked up by player 
        {
            Destroy(gameObject);
            AudioManager.Instance.ipSFX(objectCanePickUp); //when player hits Watch and presses E, Audio Plays
            pickupPanel.SetActive(true);
            CanePickUp();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.ToggleText(""); //message is blank when player is not in trigger zone
        }

    }

    public void CanePickUp()
    {
        UIManager.Instance.ToggleText(""); //message is blank when player picks up object
        //GameManager.Instance.OnCanePickUp();
    }
}
