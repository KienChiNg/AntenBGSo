using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }


    [SerializeField] private Mode mode;
    public Camera cam;
    private void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }


    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(cam.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - cam.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = cam.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -cam.transform.forward;
                break;
        }
    }

}