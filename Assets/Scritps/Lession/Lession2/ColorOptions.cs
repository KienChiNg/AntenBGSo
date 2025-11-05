using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOptions : MonoBehaviour
{
    public LineRenderer lineRendererWave1;
    public List<MeshRenderer> wave1Arrow;
    public LineRenderer lineRendererWave2;
    public List<MeshRenderer> wave2Arrow;
    public LineRenderer lineRendererWave1_2;
    public List<MeshRenderer> wave1_2Arrow;
    public Camera camera;
    public FlexibleColorPicker fcp;
    public Material material;
    public Material axes;
    public List<LineRenderer> lineRendererHelperLines;
    public Image imageClose;
    public void SetStateImageClose(bool state)
    {
        if (!state)
        {
            imageClose.sprite = Resources.Load<Sprite>("Anten/CloseNormal");
        }
        else
        {
            imageClose.sprite = Resources.Load<Sprite>("Anten/CloseDown");
        }
    }
    private void SetColorInit(Color color)
    {
        gameObject.SetActive(true);
        fcp.color = color;
    }
    public void SetColorBG()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(camera.backgroundColor);
        fcp.onColorChange.AddListener((color) =>
        {
            camera.backgroundColor = color;
        });
    }
    public void SetColorWave1()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(lineRendererWave1.material.color);

        fcp.onColorChange.AddListener((color) =>
        {
            lineRendererWave1.material.color = color;
            foreach (MeshRenderer gameObject in wave1Arrow)
                gameObject.material.color = color;
        });
    }
    public void SetColorWave2()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(lineRendererWave2.material.color);
        fcp.onColorChange.AddListener((color) =>
        {
            lineRendererWave2.material.color = color;
            foreach (MeshRenderer gameObject in wave2Arrow)
                gameObject.material.color = color;
        });
    }
    public void SetColorWave1_2()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(lineRendererWave1_2.material.color);
        fcp.onColorChange.AddListener((color) =>
        {
            lineRendererWave1_2.material.color = color;
            foreach (MeshRenderer gameObject in wave1_2Arrow)
                gameObject.material.color = color;
        });
    }
    public void SetColorMaterial()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(material.color);
        fcp.onColorChange.AddListener((color) =>
        {
            material.color = color;
        });
    }
    public void SetColorAxes()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(axes.color);
        fcp.onColorChange.AddListener((color) =>
        {
            axes.color = color;
        });
    }
    public void SetColorHelperLines()
    {
        fcp.onColorChange.RemoveAllListeners();
        SetColorInit(lineRendererHelperLines[0].material.color);
        fcp.onColorChange.AddListener((color) =>
        {
            foreach (LineRenderer lineRenderer in lineRendererHelperLines)
                lineRenderer.material.color = color;
        });
    }
    public void SetColorResetMode()
    {
        material.color = Const.ORIGINAL_COLOR_MATERIAL;
        axes.color = Const.ORIGINAL_COLOR_AXES;
        camera.backgroundColor = Const.ORIGINAL_COLOR_BG;
        lineRendererWave1.material.color = Const.ORIGINAL_COLOR_WAVE_1;
        foreach (MeshRenderer gameObject in wave1Arrow)
            gameObject.material.color = Const.ORIGINAL_COLOR_WAVE_1;
        lineRendererWave2.material.color = Const.ORIGINAL_COLOR_WAVE_2;
        foreach (MeshRenderer gameObject in wave2Arrow)
            gameObject.material.color = Const.ORIGINAL_COLOR_WAVE_2;
        lineRendererWave1_2.material.color = Const.ORIGINAL_COLOR_WAVE_1_2;
        foreach (MeshRenderer gameObject in wave1_2Arrow)
            gameObject.material.color = Const.ORIGINAL_COLOR_WAVE_1_2;
        foreach (LineRenderer lineRenderer in lineRendererHelperLines)
            lineRenderer.material.color = Const.ORIGINAL_COLOR_HELPER_LINE;
    }
}
