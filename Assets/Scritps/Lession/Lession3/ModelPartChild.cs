using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPartChild : MonoBehaviour
{
    [System.Serializable]
    public class Model3D
    {
        public GameObject model;
        public GameObject contentModel;
        public GameObject contentTextModel;
    }
    public Camera cameraMain;
    public Material darkSky;
    public Material normalSky;
    public Action<GameObject, bool> OnSelected;
    public List<Model3D> model3D;
    private void Start()
    {
        Selected(0);
    }
    public void Selected(int inx)
    {
        Reset();
        model3D[inx].model.SetActive(true);
        model3D[inx].contentModel.SetActive(true);
        model3D[inx].contentTextModel.SetActive(true);

        switch (inx)
        {
            case 0:
                cameraMain.clearFlags = CameraClearFlags.Skybox;
                RenderSettings.skybox = darkSky;
                break;
            case 1:
                cameraMain.clearFlags = CameraClearFlags.SolidColor;
                cameraMain.backgroundColor = new Color32(218, 231, 253, 0);
                RenderSettings.skybox = normalSky;
                break;
            case 2:
                cameraMain.clearFlags = CameraClearFlags.SolidColor;
                cameraMain.backgroundColor = new Color32(218, 231, 253, 0);
                RenderSettings.skybox = normalSky;
                break;
        }

        bool isRotate = true;
        if (inx == 2) isRotate = false;
        OnSelected?.Invoke(model3D[inx].model, isRotate);
    }
    public void Reset()
    {
        for (int i = 0; i < model3D.Count; i++)
        {
            model3D[i].model.SetActive(false);
            model3D[i].contentModel.SetActive(false);
            model3D[i].contentTextModel.SetActive(false);
        }
    }
}
