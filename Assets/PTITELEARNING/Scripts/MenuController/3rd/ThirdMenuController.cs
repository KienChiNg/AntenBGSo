using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdMenuController : MonoBehaviour
{
    public GameObject[] model3D;
    public GameObject[] contentModel3D;
    public GameObject UImodel3D;
    public Text title;
    public RotateModelWithMouse rotateModelScript;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < model3D.Length; i++)
        {
            model3D[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chonModel(int num)
    {
        rotateModelScript.resetView3D();
        UImodel3D.SetActive(true);
        for (int i = 0; i < model3D.Length; i++)
        {
            model3D[i].SetActive(false);
            contentModel3D[i].SetActive(false);
        }
        model3D[num].SetActive(true);
        contentModel3D[num].SetActive(true);
        title.text = contentModel3D[num].name;

    }

    public void closeModel()
    {
        UImodel3D.SetActive(false);
        for (int i = 0; i < model3D.Length; i++)
        {
            model3D[i].SetActive(false);
            contentModel3D[i].SetActive(false);
        }
    }
}
