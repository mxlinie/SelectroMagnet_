using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    public string message;

    [SerializeField] private AudioClip objectPickUp;

    public GameObject pickupPanel;

    void OnTriggerStay(Collider other)
    {
        UIManager.Instance.ToggleText(message); //Message set in the inspector
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) //if player is near watch object can be picked up by player 
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
            AudioManager.Instance.ipSFX(objectPickUp); //when player hits Watch and presses E, Audio Plays
            Debug.Log(name);
            pickupPanel.SetActive(true);
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
        //StartCoroutine(PickUpPanel());
    }

    /*IEnumerator PickUpPanel()
    {
        yield return new WaitForSeconds(1f);
        pickupPanel.SetActive(true);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
