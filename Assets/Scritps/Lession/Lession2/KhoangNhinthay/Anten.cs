using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anten : MonoBehaviour
{
    [SerializeField] private GameObject topAnten;
    public Transform center;          // Tâm của đường tròn
    public float radius;         // Bán kính của đường tròn
    private float scale;
    private void Start()
    {
        scale = transform.localScale.y;
        // SetHeightAnten(0);
        // CalPosAnten(-17.25603f);
    }
    public Vector3 GetTopAnten()
    {
        return topAnten.transform.position;
    }
    public void SetHeightAnten(float height)
    {
        float scaleAnten = height / 1000 * 2;
        transform.localScale = new Vector3(scaleAnten, scaleAnten, scaleAnten);
    }
    public void CalPosAnten(float angle)
    {
        // Debug.Log(angle);
        // Tính toán góc xoay theo thời gian
        // Chuyển đổi góc từ độ sang radian
        float angleRad = (angle + 90) * Mathf.Deg2Rad;

        // Tính toán vị trí mới của vật thể dựa trên hàm sin và cos
        float x = center.localPosition.x + Mathf.Cos(angleRad) * radius;
        float y = center.localPosition.z + Mathf.Sin(angleRad) * radius;

        // Cập nhật vị trí của vật thể
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

}
