using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LessonReference;

public class Router : MonoBehaviour
{
    public static Router instance;
    public Lesson currentLesson;
    public string subLesson;
    public string details;
    public string currentScene = "Home";

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
       
    }
    
    public void NavigateLessonDetails(string subLesson)
    {
        this.subLesson = subLesson;
        LoadScene("LessonDetails");
    }

    public void NavigateView3DContent(string details)
    {
        this.details = details;
        LoadScene("View3DContent");
    }

    public void NavigateScene(string scene)
    {
        this.details = "other";
        LoadScene(scene);
    }

    public void LoadScene(string curScene)
    {
        currentScene = curScene;
        //load
        if(curScene == "Home")
        {
            this.subLesson = "";
            this.details = "";
        }

        SceneManager.LoadScene(currentScene);
    }

    public void GoBack()
    {
        switch(currentScene)
        {
            case "Home":
                break;
            case "LessonDetails":

                LoadScene("Home");
                break;
            case "View3DContent":
                if(subLesson == "")
                {
                    LoadScene("Home");
                } else
                {
                    LoadScene("LessonDetails");
                }
                
                break;
            case "other":
                LoadScene("LessonDetails");
                break;
                
        }
    }
}
