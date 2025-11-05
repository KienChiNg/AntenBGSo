using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSubLessonDetails_1 : MonoBehaviour
{
    public Text title;
    public Image image;

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpComponent(JSONObject o)
    {
        title.text = o.GetField("name").str;
        image.sprite = Resources.Load<Sprite>(o.GetField("image").str);

        //set btn Event
        GetComponent<SetUpButtonEvent>().SetEvent(o);
    }
}
