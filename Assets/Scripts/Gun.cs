using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    //public Material p_Material;

    public GameObject positivePolarityObject;
    public GameObject negativePolarityObject;

    public GameObject firePoint;

    [SerializeField] private AudioClip leftMouseClick;
    [SerializeField] private AudioClip rightMouseClick;
    [SerializeField] private AudioClip magnetSuccess;

    [System.NonSerialized] public bool tweening;
    private Vector3 dampVelocity;

    // Update is called once per frame

   // https://answers.unity.com/questions/19747/simultaneous-single-and-double-click-functions.html;

    
    void FixedUpdate()
    {
        //int layerMask = 1 << LayerMask.NameToLayer("MyCustomRaycastLayer");
        //layerMask = ~layerMask;


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.transform.position, transform.right, out hit))
             
            {
                AudioManager.Instance.lmSFX(leftMouseClick); //when left mouse click occurs AudioManager script will make the audio source and play sound
                Debug.DrawRay(firePoint.transform.position, transform.right, Color.green, 2f);
                print("Found an object - distance: " + hit.distance);
                if(hit.collider.gameObject.GetComponent<Polarity>()!=null)
                {
                    if (hit.collider.gameObject.GetComponent<Polarity>().thisPole != Pole.Positive)
                    {
                        if(positivePolarityObject != null)
                        {
                            ResetPolarity(positivePolarityObject);
                        }
                        Debug.Log("Positive");
                        //print(hit.collider.gameObject.GetComponent<MeshRenderer>().materials.Length);
                        //hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetInt("Pack_03_M_P", 1);
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color32(58, 125, 251, 255); //Changes Albedo to blue
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Positive);
                        positivePolarityObject = hit.collider.gameObject;
                        StartCoroutine(MoveObjects());
                    }

                 
                    else
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255); //Changes Albedo back to white
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Neutral);
                        positivePolarityObject = null;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.transform.position, transform.right, out hit))
            {
                AudioManager.Instance.lmSFX(rightMouseClick); //right mouse click plays audio clip
                Debug.DrawRay(firePoint.transform.position, transform.right, Color.green, 2f);
                print("Found an object - distance: " + hit.distance);
                if (hit.collider.gameObject.GetComponent<Polarity>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Polarity>().thisPole != Pole.Negative)
                    {
                        if (negativePolarityObject != null)
                        {
                            ResetPolarity(negativePolarityObject);
                        }
                        Debug.Log("Negative");
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color32(255, 88, 88, 255); //Changes Albedo to red
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Negative);
                        negativePolarityObject = hit.collider.gameObject;
                        StartCoroutine(MoveObjects());
                    }

                   
                    else
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255); //Changes Albedo back to white
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Neutral);
                        negativePolarityObject = null;
                    }

                }
            }
        }
       
    }

    IEnumerator MoveObjects()
    {
        if(positivePolarityObject != null && negativePolarityObject != positivePolarityObject && negativePolarityObject != null)
        {
            Debug.Log("Success");
            tweening = true;

            while(tweening && Vector3.Distance(negativePolarityObject.transform.position, positivePolarityObject.transform.position) > .1F)
            {
                positivePolarityObject.transform.position = Vector3.SmoothDamp(positivePolarityObject.transform.position, negativePolarityObject.transform.position, ref dampVelocity, 1F);

                yield return 0;
            }

            SetNull();
            tweening = false;

            //currentTween = positivePolarityObject.transform.DOMove(negativePolarityObject.transform.position, 2).OnComplete(SetNull);

            //negativePolarityObject.transform.DOMove(positivePolarityObject.transform.position, 2).OnComplete(SetNull);
        AudioManager.Instance.msSFX(magnetSuccess);
        }

        yield return 0;
    }

    public void OnPlatformCollided(GameObject source, GameObject other)
    {
        if(source == positivePolarityObject && other == negativePolarityObject)
        {
            tweening = false;
        }
    }

    void SetNull()
    {

        // Want to create parenting for any object attached to moving platform.
        //Need to check that the parent has to have the Platform script attached to it. If not, it will interfere with game function.
        //Parenting has to occur the instant that two objects make contact with each other.
        Debug.Log("Complete");
        if(negativePolarityObject)
            ResetPolarity(negativePolarityObject);
        if(positivePolarityObject)
            ResetPolarity(positivePolarityObject);
        positivePolarityObject = null;
        negativePolarityObject = null;
    }

    void ResetPolarity (GameObject platform)
    {
        platform.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255); //Changes Albedo back to white
        platform.GetComponent<Polarity>().SetPole(Pole.Neutral);
        
    }

}
