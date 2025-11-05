using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DopplerManager : MonoBehaviour
{
    [System.Serializable]
    public class Model3D
    {
        public GameObject model;
        public GameObject contentModel;
    }
    public List<Model3D> model3D;
    public Camera cameraMain;
    public Camera cameraTexture;
    public Material darkSky;
    public Material normalSky;
    // public Vector3 initPosCam;
    // public Quaternion initRotCam;
    private void Start()
    {
        Selected(0);
        // initPosCam = cameraTexture.transform.localPosition;
        // initRotCam = cameraTexture.transform.localRotation;
    }
    public void Selected(int inx)
    {
        //Debug.Log("KC" + inx);
        Reset();
        model3D[inx].model.SetActive(true);
        model3D[inx].contentModel.SetActive(true);
        switch (inx)
        {
            case 0:
                cameraMain.clearFlags = CameraClearFlags.Skybox;
                RenderSettings.skybox = darkSky;
                // cameraTexture.transform.position = new Vector3(0, 0,)
                break;
            case 1:
                cameraMain.clearFlags = CameraClearFlags.SolidColor;
                cameraMain.backgroundColor = new Color32(218, 231, 253, 0);
                RenderSettings.skybox = normalSky; 
                break;
        }

        // cameraTexture.transform.localPosition = initPosCam;
        // cameraTexture.transform.localRotation = initRotCam;
    }
    public void Reset()
    {
        for (int i = 0; i < model3D.Count; i++)
        {
            model3D[i].model.SetActive(false);
            model3D[i].contentModel.SetActive(false);
        }
    }
}
