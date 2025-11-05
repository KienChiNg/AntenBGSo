using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public static HomeManager instance;
    public List<LessionPart> lessionParts;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        SetUpInxScene();
    }
    private void SetUpInxScene()
    {
        for (int i = 0; i < lessionParts.Count; i++)
        {
            lessionParts[i].inxScene = i + 1;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
