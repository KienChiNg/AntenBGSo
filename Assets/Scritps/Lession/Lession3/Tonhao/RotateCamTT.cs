using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamTT : MonoBehaviour
{
    public Camera cam;
    public Transform target; // Vật thể mà camera sẽ xoay quanh
    public float distance = 5.0f; // Khoảng cách từ camera tới vật thể
    public float xSpeed = 240.0f; // Tốc độ xoay ngang
    public float ySpeed = 240.0f; // Tốc độ xoay dọc

    public float yMinLimit = -20f; // Giới hạn dưới cho góc quay dọc
    public float yMaxLimit = 80f;  // Giới hạn trên cho góc quay dọc

    private float x = 0.0f; // Biến lưu góc quay ngang
    private float y = 0.0f; // Biến lưu góc quay dọc

    void Start()
    {
        // Lấy góc hiện tại của camera
        Vector3 angles = cam.transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Đặt chế độ con trỏ chuột để dễ xoay camera
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        distance = Vector3.Distance(cam.transform.position, target.position);
        RotateCam();
    }

    void LateUpdate()
    {
        if (target != null && Input.GetMouseButton(1))
        {
            RotateCam();
        }
    }
    private void RotateCam()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

        cam.transform.rotation = rotation;
        cam.transform.position = position;
    }
}
