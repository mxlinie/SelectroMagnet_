using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pole { Neutral, Positive, Negative}

public class Polarity : MonoBehaviour
{
    //public Material[] matList = new Material[2];  

    public Pole thisPole;

    void Start()
    {
        //gameObject.GetComponent<MeshRenderer>().material = matList[1];
    }

    public void SetPole(Pole newPole)
    {

        if (thisPole != newPole)
        {
            if (thisPole != newPole)
                thisPole = newPole;
        }
    }
}
