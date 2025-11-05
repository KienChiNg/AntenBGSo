using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 30f;
    private float currentX = 0.0f;
    private Vector3 v3;
    public ModelPartChild modelPartChild;
    private bool isRotate = true;
    private float distance = 5.0f;
    private float xSpeed = 240.0f; 
    private float ySpeed = 240.0f; 

    private float yMinLimit = -20f; 
    private float yMaxLimit = 80f;  

    private float x = 0.0f; 
    private float y = 0.0f; 
    private void Start()
    {
        modelPartChild.OnSelected += OnSelected;
    }

    private void OnSelected(GameObject @object, bool isRotate)
    {
        transform.rotation = Quaternion.Euler(12, 0, 0);
        transform.position = new Vector3(0, -0.37f, 0);
        this.isRotate = isRotate;
        target = @object.transform;
        // distance = Vector3.Distance(transform.position, target.position)*1.2f;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && isRotate)
        {
            currentX = Input.GetAxis("Mouse X") * rotationSpeed;
            v3 = new Vector3(0, currentX, 0);
            transform.RotateAround(target.position, v3, rotationSpeed * Time.deltaTime);
            // RotateCam();
        }
    }
    private void RotateCam()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, currentX, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
