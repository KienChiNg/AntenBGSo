using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class menuControllerAR : MonoBehaviour
{
    Animator dongcodayduAnimator;
    Animator dongcorutgonAnimator;
    Animator chartAnimator;
    public Button pauseBtn;
    public Button playBtn;
    public GameObject dongcodaydu;
    public GameObject dongcorutgon;
    public GameObject chart;
    // Start is called before the first frame update
    void Start()
    {
        dongcodayduAnimator = dongcodaydu.GetComponent<Animator>();
        dongcorutgonAnimator = dongcorutgon.GetComponent<Animator>();
        chartAnimator = chart.GetComponent<Animator>();

        playBtn.gameObject.SetActive(false);
        pauseBtn.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAnim()
    {
        dongcodayduAnimator.speed = 1;
        dongcorutgonAnimator.speed = 1;
        chartAnimator.speed = 1;

        playBtn.gameObject.SetActive(false);
        pauseBtn.gameObject.SetActive(true);
    }

    public void pauseAnim()
    {
        dongcodayduAnimator.speed = 0;
        dongcorutgonAnimator.speed = 0;
        chartAnimator.speed = 0;

        playBtn.gameObject.SetActive(true);
        pauseBtn.gameObject.SetActive(false);
    }
}
