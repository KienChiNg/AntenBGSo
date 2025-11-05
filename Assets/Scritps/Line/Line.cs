using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Transform origin;
    public Transform destination;
    LineRenderer lr;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        lr.SetPosition(0, origin.position);
        lr.SetPosition(1, destination.position);
    }
}
