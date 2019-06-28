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

    // Update is called once per frame



    
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
                        //print(hit.collider.gameObject.GetComponent<MeshRenderer>().materials.Length);
                        //hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetInt("Pack_03_M_P", 1);
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Positive);
                        positivePolarityObject = hit.collider.gameObject;
                        StartCoroutine(MoveObjects());
                    }
                    else
                    {
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
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Negative);
                        negativePolarityObject = hit.collider.gameObject;
                        MoveObjects();
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Polarity>().SetPole(Pole.Neutral);
                        negativePolarityObject = null;
                    }

                }
            }
        }
       
    }

    IEnumerator MoveObjects()
    {
        if(positivePolarityObject != null && negativePolarityObject != null)
        {
            
            positivePolarityObject.transform.DOMove(negativePolarityObject.transform.position, 2).OnComplete(SetNull);
            //negativePolarityObject.transform.DOMove(positivePolarityObject.transform.position, 2).OnComplete(SetNull);
        AudioManager.Instance.msSFX(magnetSuccess);
        }

        yield return 0;
    }

    void SetNull()
    {
        positivePolarityObject = null;
        negativePolarityObject = null;
    }


}
