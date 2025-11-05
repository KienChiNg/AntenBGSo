using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopperHover : MonoBehaviour
{
    public Camera renderTextureCamera;
    public float rayLength = 200f;
    public LayerMask clickableLayer;
    public RawImage rawImage;
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = rawImage.GetComponent<RectTransform>();
    }
    void Update()
    {

        // Kiểm tra nếu vị trí chuột nằm trong vùng của RawImage
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPoint))
        {
            // Kiểm tra nếu vị trí chuột nằm trong khu vực của RawImage
            if (localPoint.x >= rectTransform.rect.xMin && localPoint.x <= rectTransform.rect.xMax &&
                localPoint.y >= rectTransform.rect.yMin && localPoint.y <= rectTransform.rect.yMax)
            {
                // Chuyển đổi localPoint sang viewportPoint của camera
                Vector2 viewportPoint = new Vector2(
                    (localPoint.x - rectTransform.rect.xMin) / rectTransform.rect.width,
                    (localPoint.y - rectTransform.rect.yMin) / rectTransform.rect.height);

                // Raycast từ camera sử dụng viewportPoint
                Ray ray = renderTextureCamera.ViewportPointToRay(viewportPoint);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
                {
                    // Debug.Log("Hit object: " + hit.collider.gameObject.name);
                }
            }
        }
    }
}
