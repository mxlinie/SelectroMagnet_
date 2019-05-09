using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    public string message;

    [SerializeField] private AudioClip objectPickUp;

    void OnTriggerStay(Collider other)
    {
        UIManager.Instance.ToggleText(message); //Message set in the inspector
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) //if player is near watch object can be picked up by player 
        {
            Destroy(gameObject);
            AudioManager.Instance.ipSFX(objectPickUp); //when player hits Watch and presses E, Audio Plays
            Debug.Log(name);
            PickUp();
        }

    }

    void OnTriggerExit(Collider other)
    {
        //lockText.gameObject.SetActive(true);
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.ToggleText(""); //message is blank when player is not in trigger zone
        }

    }

    void PickUp()
    {
        UIManager.Instance.ToggleText(""); //message is blank when player picks up object
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
