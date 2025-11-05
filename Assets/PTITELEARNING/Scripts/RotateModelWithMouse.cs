using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateModelWithMouse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private Vector3 _rotation2;
    private bool _isRotating;
    public GameObject groupCon;
    public GameObject groupCha;
    public Slider slider;
    public ContentNavigator contentNavigator;
    float maxScale = 0.8f;
    float zoom = 0;
    void Start()
    {
        _sensitivity = 0.5f;
        _rotation = Vector3.zero;
        //
        // slider.onValueChanged.AddListener(delegate
        // {
        //     groupCon.transform.localScale = new Vector3(1, 1, 1) + new Vector3(slider.value * maxScale, slider.value * maxScale, slider.value * maxScale);
        // });
    }

    void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0 && contentNavigator != null && contentNavigator.currentContent != 2)
        {
            zoom += zoomDelta;
            zoom = Mathf.Clamp(zoom, 0, 1);
            groupCon.transform.localScale = new Vector3(1, 1, 1) + new Vector3(zoom * maxScale, zoom * maxScale, zoom * maxScale);
        }
        if (contentNavigator != null && contentNavigator.currentContent == 2)
            groupCon.transform.localScale = new Vector3(1, 1, 1);
        if (_isRotating && Input.GetMouseButton(1))
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.x = (_mouseOffset.y) * _sensitivity;
            //_rotation.y = -(_mouseOffset.x) * _sensitivity;
            //_rotation.z = -(_mouseOffset.y) * _sensitivity;

            _rotation2.y = -(_mouseOffset.x) * _sensitivity;

            // rotate
            groupCha.transform.Rotate(_rotation);
            // rotate group con
            groupCon.transform.Rotate(_rotation2);

            // store mouse
            _mouseReference = Input.mousePosition;
        }

        if (Input.GetKeyDown("a"))
        {
            resetView3D();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // rotating flag
        _isRotating = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    public void resetView3D()
    {
        groupCha.transform.eulerAngles = new Vector3(0, 0, 0);
        groupCon.transform.eulerAngles = new Vector3(0, 0, 0);
        // slider.value = 0;
    }
}
