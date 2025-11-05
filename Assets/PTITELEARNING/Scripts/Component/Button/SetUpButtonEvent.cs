using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpButtonEvent : MonoBehaviour
{
    public void SetEvent(JSONObject o)
    {
        //set btn Event
        switch (o.GetField("event").str)
        {
            case "LessonDetails":
                GetComponent<Button>().onClick.AddListener(delegate {
                    Router.instance.NavigateLessonDetails(o.GetField("code").str);
                });
                break;
            case "View3DContent":
                GetComponent<Button>().onClick.AddListener(delegate {
                    Router.instance.NavigateView3DContent(o.GetField("code").str);
                });
                break;
            default:
                GetComponent<Button>().onClick.AddListener(delegate {
                    Router.instance.NavigateScene(o.GetField("event").str);
                });
                break;
        }

    }
}
