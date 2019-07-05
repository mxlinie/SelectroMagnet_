using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();

    public List<Vector3> orgPos = new List<Vector3>();

    //pubic gamobject attached to gun.cs
    public GameObject selGun;

    // Start is called before the first frame update
    void Start()
    {
        //orgPosPlatforms = transform.position;
        foreach (GameObject polPlat in platforms)
        {
            orgPos.Add(polPlat.transform.localPosition);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPol();
            Debug.Log("key pressed");
        }
    }

    public void ResetPol()
    {
        int i = 0;
        //Set null reference
        selGun.GetComponent<Gun>().positivePolarityObject = null;
        selGun.GetComponent<Gun>().negativePolarityObject = null;
        
        foreach (GameObject polPlat in platforms)
        {
            Debug.Log("Index "+i+" Current pos = "+polPlat.transform.localPosition+" -- Target pos = " + orgPos[i]);
            polPlat.transform.localPosition = orgPos[i];
            polPlat.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            polPlat.GetComponent<Polarity>().thisPole = Pole.Neutral;
            i++;
        }
    }
}

