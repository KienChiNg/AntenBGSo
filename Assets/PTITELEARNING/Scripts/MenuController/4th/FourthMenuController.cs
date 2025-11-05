using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourthMenuController : MonoBehaviour
{
    public Transform groupContent;
    public Transform groupTab;
    public Button nextBtn;
    public Button prevBtn;
    public Button[] buttonContent;
    public GameObject[] content3D;
    int currentQuestion = 0;
    public Scrollbar scrollBar;
    public RotateModelWithMouse rotateModelScript;
   
    // Start is called before the first frame update
    void Start()
    {
        Init();
        //add event listener
        prevBtn.onClick.AddListener(() => {
            currentQuestion -= 1;
            QuestionBtn(currentQuestion);
        });

        nextBtn.onClick.AddListener(() => {
            currentQuestion += 1;
            QuestionBtn(currentQuestion);
        });
    }

    private void Update()
    {

        //disable prev btn if current question = 0
        if(currentQuestion == 0)
        {
            prevBtn.interactable = false;
        } else
        {
            prevBtn.interactable = true;
        }

        //disable next btn if current question = length
        if (currentQuestion == groupContent.childCount - 1)
        {
            nextBtn.interactable = false;
        } else
        {
            nextBtn.interactable = true;
        }
    }

    public void Init()
    {
        //Gan index cho button
        for(int i = 0; i < groupContent.childCount; i ++)
        {
            int index = i;
            buttonContent[i].GetComponent<Button>().onClick.AddListener(() => { QuestionBtn(index); });
        }

        //select question 1
        QuestionBtn(0);
    }


    public void QuestionBtn(int index)
    {
        rotateModelScript.resetView3D();
        currentQuestion = index;
        //
        for (int i = 0; i < groupTab.childCount; i++)
        {
            groupTab.GetChild(i).GetComponent<Image>().color = new Color32(152, 58, 58, 255);
        }
        //
        groupTab.GetChild(index).GetComponent<Image>().color = new Color32(249, 65, 65, 255);
        //
        for(int i = 0; i < groupContent.childCount; i ++)
        {
            groupContent.GetChild(i).gameObject.SetActive(false);
            content3D[i].gameObject.SetActive(false);
        }
        groupContent.GetChild(index).gameObject.SetActive(true);
        content3D[index].gameObject.SetActive(true);
        //update scroll bar
        float scrollVal = (1f / groupContent.childCount) * (index + 1);

        if(scrollVal > scrollBar.size)
        {
            scrollBar.value = (scrollVal - scrollBar.size) / (1 - scrollBar.size);
        } else
        {
            scrollBar.value = 0;
        }
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }
}
