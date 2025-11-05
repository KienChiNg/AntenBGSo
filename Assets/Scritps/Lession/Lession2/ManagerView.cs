using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagerView : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;
    [SerializeField] private List<GameObject> textureCams;
    [SerializeField] private GameObject UI;
    private Vector3 previousPosition;
    private float minZoomFOV = 5f; // Góc nhìn tối thiểu (zoom out)
    private float maxZoomFOV = 50f; // Góc nhìn tối đa (zoom in)
    private float zoomSpeed = 10f; // Tốc độ zoom

    private void Start()
    {
        Nice();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            textureCams[0].SetActive(true);
            textureCams[1].SetActive(false);
            UI.SetActive(true);
        }

        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        if (zoomInput != 0)
        {
            // Tính toán và cập nhật góc nhìn của camera
            float newFOV = cam.fieldOfView - zoomInput * zoomSpeed;
            newFOV = Mathf.Clamp(newFOV, minZoomFOV, maxZoomFOV); // Giới hạn góc nhìn giữa min và max
            cam.fieldOfView = newFOV;
        }
    }
    public void Nice()
    {
        cam.transform.position = new Vector3(84.72272f, 29.87111f, -79.5599f);
        // Tạo vectơ vị trí từ giá trị float
        Vector3 initialPosition = new Vector3(14.414f, -46.8f, 0);
        // Sử dụng SetPositionAndRotation để đặt vị trí và quay ban đầu
        cam.transform.localEulerAngles = initialPosition;
    }
    public void Side()
    {
        cam.transform.position = new Vector3(0f, 0f, -120f);
        // Tạo vectơ vị trí từ giá trị float
        Vector3 initialPosition = new Vector3(0f, 0f, 0);
        // Sử dụng SetPositionAndRotation để đặt vị trí và quay ban đầu
        cam.transform.localEulerAngles = initialPosition;
    }
    public void Front()
    {
        cam.transform.position = new Vector3(120, 0f, 0f);
        // Tạo vectơ vị trí từ giá trị float
        Vector3 initialPosition = new Vector3(0f, -90f, 0);
        // Sử dụng SetPositionAndRotation để đặt vị trí và quay ban đầu
        cam.transform.localEulerAngles = initialPosition;
    }
    public void Back()
    {
        cam.transform.position = new Vector3(120, 0f, 0f);
        // Tạo vectơ vị trí từ giá trị float
        Vector3 initialPosition = new Vector3(0f, 90f, 0);
        // Sử dụng SetPositionAndRotation để đặt vị trí và quay ban đầu
        cam.transform.localEulerAngles = initialPosition;
    }
    public void FullScreen()
    {
        textureCams[0].SetActive(false);
        textureCams[1].SetActive(true);
        UI.SetActive(false);
    }
    public void CreateGIF()
    {
        // Bắt đầu ghi lại khi nút được nhấn
        // StartCoroutine(RecordFrames());
    }

    // IEnumerator RecordFrames()
    // {
    //     while (recordingStarted && frameCount < framesToRecord)
    //     {
    //         // Ghi lại frame từ RawImage
    //         Texture2D texture = new Texture2D((int)rawImage.rectTransform.rect.width, (int)rawImage.rectTransform.rect.height, TextureFormat.RGB24, false);
    //         texture.ReadPixels(new Rect(rawImage.rectTransform.position.x, rawImage.rectTransform.position.y, rawImage.rectTransform.rect.width, rawImage.rectTransform.rect.height), 0, 0);
    //         texture.Apply();

    //         // Lưu frame vào danh sách
    //         frames.Add(texture);
    //         frameCount++;

    //         // Chờ 1 frame trước khi ghi lại frame tiếp theo
    //         yield return null;
    //     }

    //     if (frameCount >= framesToRecord)
    //     {
    //         // Tổng hợp các hình ảnh thành GIF
    //         string gifPath = Path.Combine(outputFolder, "MyGif.gif");
    //         GifHelper.SaveGif(gifPath, frames.ToArray(), frameRate);

    //         // Dừng việc ghi lại
    //         recordingStarted = false;
    //     }
    // }
}