using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMenuController : MonoBehaviour
{
    [Header("Models")]
    public GameObject dongcodaydu;
    public GameObject dongcorutgon;
    public GameObject floor;
    Animator dongcodayduAnimator;
    Animator dongcorutgonAnimator;
    Animator chartAnimator;
    [Header("UI")]
    public Toggle dayduToggle;
    public Toggle rutgonToggle;
    public Button pauseBtn;
    public Button playBtn;
    public GameObject chart;
    [Header("Content Tab")]
    public List<NoiDungLyThuyet> contents;
    public int currentTab = 0;
    public Button nextBtn;
    public Button prevBtn;
    public Text title;
    public GameObject contentScroll;

    // Start is called before the first frame update
    void Start()
    {
        dongcodayduAnimator = dongcodaydu.GetComponent<Animator>();
        dongcorutgonAnimator = dongcorutgon.GetComponent<Animator>();
        chartAnimator = chart.GetComponent<Animator>();
        Init();
        //add event listener
        AddEventListenerToGameobject();
    }

    private void Update()
    {
        //disable prev button if current tab is 0
        if(currentTab == 0)
        {
            prevBtn.interactable = false;
        } else
        {
            prevBtn.interactable = true;
        }

        //disable next button if current tab is final
        if(currentTab == contents.Count - 1)
        {
            nextBtn.interactable = false;
        } else
        {
            nextBtn.interactable = true;
        }
    }

    public void Init()
    {
        contents = new List<NoiDungLyThuyet>();
        //load reference 
        TextAsset text = Resources.Load<TextAsset>("NguyenLy/reference");
        string[] references = text.text.Split('\n');

        for(int i = 0; i < references.Length; i ++)
        {
            string[] texts = references[i].Split('|');
            //
            NoiDungLyThuyet ndlt = new NoiDungLyThuyet();
            ndlt.title = texts[0];
            ndlt.prefab = Resources.Load<GameObject>("NguyenLy/" + texts[1].Trim());
            //
            contents.Add(ndlt);
        }

        //load tab content
        LoadContentTab();
    }

    public void AddEventListenerToGameobject()
    {
        dayduToggle.onValueChanged.AddListener(delegate {
            DisplayModel(dayduToggle.isOn);
        });

        rutgonToggle.onValueChanged.AddListener(delegate {
            DisplayModel(!rutgonToggle.isOn);
        });

        pauseBtn.onClick.AddListener(delegate
        {
            pauseBtn.gameObject.SetActive(false);
            playBtn.gameObject.SetActive(true);
            ChangeAnimationSpeed(0);
        });

        playBtn.onClick.AddListener(delegate
        {
            pauseBtn.gameObject.SetActive(true);
            playBtn.gameObject.SetActive(false);
            ChangeAnimationSpeed(1);
        });

        //next button
        nextBtn.onClick.AddListener(delegate
        {
            currentTab += 1;
            LoadContentTab();
        });

        //prev button
        prevBtn.onClick.AddListener(delegate
        {
            currentTab -= 1;
            LoadContentTab();
        });
    }

    public void LoadContentTab()
    {
        //clear previous content
        Destroy(contentScroll.transform.GetChild(0).gameObject);
        //add new content
        title.text = (currentTab + 1) + ". " + contents[currentTab].title;
        Instantiate(contents[currentTab].prefab, contentScroll.transform);
    }

    public void ChangeAnimationSpeed(float speed)
    {
        if(dongcodayduAnimator)
        {
            dongcodayduAnimator.speed = speed;
        }

        if(dongcorutgonAnimator)
        {
            dongcorutgonAnimator.speed = speed;
        }

        if(chartAnimator)
        {
            chartAnimator.speed = speed;
        }
    }

    public void DisplayModel(bool details)
    {
        floor.SetActive(true);
        //reset animation speed
        ChangeAnimationSpeed(1);

        //disable toggle, model
        dongcodaydu.SetActive(false);
        dongcorutgon.SetActive(false);
        chart.SetActive(false);
        pauseBtn.gameObject.SetActive(true);
        playBtn.gameObject.SetActive(false);

        //enable toggle, model
        if (details)
        {
            rutgonToggle.isOn = false;
            dongcodaydu.SetActive(true);
        } else
        {
            dayduToggle.isOn = false;
            dongcorutgon.SetActive(true);
            chart.SetActive(true);
        }
    }


    private void OnEnable()
    {
        rutgonToggle.isOn = true;
        dayduToggle.isOn = true;
        StartCoroutine(delayDayDu());
        DisplayModel(false);
    }

    private void OnDisable()
    {
        dongcodaydu.SetActive(false);
        dongcorutgon.SetActive(false);
        floor.SetActive(false);
    }

    private IEnumerator delayDayDu()
    {
        yield return new WaitForSeconds(0.05f);
        dayduToggle.isOn = false;
    }
}

public class NoiDungLyThuyet
{
    public string title;
    public GameObject prefab;
}