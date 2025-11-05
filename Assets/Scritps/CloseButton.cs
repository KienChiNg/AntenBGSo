using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    private Image _imgClose;
    private void Awake()
    {
        _imgClose = GetComponent<Image>();
    }
    private void Start()
    {
        // #if UNITY_STANDALONE_WIN
        //         _imgClose.sprite = Resources.Load<Sprite>("Anten/CloseNormal");
        // #endif 
        // #if UNITY_WEBGL || UNITY_EDITOR
        //         _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomOutNormal");
        // #endif
    }
    private void OnEnable()
    {
#if UNITY_STANDALONE_WIN
        _imgClose.sprite = Resources.Load<Sprite>("Anten/CloseNormal");
#endif
#if UNITY_WEBGL || UNITY_EDITOR
        if (FullscreenWebGL.isFullscreenSupported())
        {
            FullscreenWebGL.subscribeToFullscreenchangedEvent();
            FullscreenWebGL.onfullscreenchange += Init;
        }
        if (!Config.isZoom)
            _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomOutNormal");
        else
            _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomInNormal");
#endif
    }
    private void OnDisable()
    {
#if UNITY_WEBGL || UNITY_EDITOR
        if (FullscreenWebGL.isFullscreenSupported())
        {
            FullscreenWebGL.subscribeToFullscreenchangedEvent();
            FullscreenWebGL.onfullscreenchange -= Init;
        }
#endif
    }
    public void Init()
    {
        if (FullscreenWebGL.isFullscreen())
        {
            _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomInNormal");
            Config.isZoom = true;
        }
        else
        {
            _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomOutNormal");
            Config.isZoom = false;
        }
    }
    public void OnPointerDown()
    {
#if UNITY_STANDALONE_WIN
        _imgClose.sprite = Resources.Load<Sprite>("Anten/CloseDown");
#endif
#if UNITY_WEBGL || UNITY_EDITOR
        StartCoroutine(QuitAsync());
#endif
    }

    IEnumerator QuitAsync()
    {
        yield return ChangeBtnSprite();
        Quit();
    }
    IEnumerator ChangeBtnSprite()
    {
        if (Config.isZoom)
            _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomInSelected");
        else
            _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomOutSelected");
        yield return null;
    }
    public void OnPointerUp()
    {
#if UNITY_STANDALONE_WIN
        _imgClose.sprite = Resources.Load<Sprite>("Anten/CloseNormal");
        Quit();
#endif
#if UNITY_WEBGL || UNITY_EDITOR
#endif
    }

    public void Quit()
    {
#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
#if UNITY_WEBGL 
         if (Config.isZoom)
        {
            Exitfullscreen();
        }
        else
        {
            Enterfullscreen();
        }
#endif
    }
    public void Enterfullscreen()
    {
        FullscreenWebGL.requestFullscreen(stat =>
        {
            if (stat == FullscreenWebGL.status.Success)
            {
                _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomInNormal");
                Config.isZoom = true;
            }
        }, FullscreenWebGL.navigationUI.hide);
    }

    public void Exitfullscreen()
    {
        FullscreenWebGL.exitFullscreen(stat =>
        {
            if (stat == FullscreenWebGL.status.Success)
            {
                _imgClose.sprite = Resources.Load<Sprite>("Anten/ZoomOutNormal");
                Config.isZoom = false;
            }
        });
    }
}
