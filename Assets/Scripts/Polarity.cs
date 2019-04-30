using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pole { Neutral, Positive, Negative}

public class Polarity : MonoBehaviour
{
    public Pole thisPole;
    public void SetPole(Pole newPole)
    {

        if (thisPole != newPole)
        {
            if (thisPole != newPole)
                thisPole = newPole;
        }
    }
}
