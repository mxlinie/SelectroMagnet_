using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserBeamLength;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() //The Line will now be constantly rendered while following player movement
    {
        Vector3 endPosition = transform.position + (transform.right * laserBeamLength);
        lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });
    }

}
