using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Image _imgBack;
    private void Awake()
    {
        _imgBack = GetComponent<Image>();
    }
    private void Start() {
        _imgBack.sprite = Resources.Load<Sprite>("Anten/BackNormal");
    }
    public void OnPointerDown()
    {
        _imgBack.sprite = Resources.Load<Sprite>("Anten/BackDown");
    }
    public void OnPointerUp()
    {
        _imgBack.sprite = Resources.Load<Sprite>("Anten/BackNormal");
    }
    public void BackToHome()
    {
        SceneManager.LoadScene(0);
    }
}
