using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSubLesson_1 : MonoBehaviour
{
    public Text index;
    public Text title;
    public Image image;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpComponent(JSONObject o, int index)
    {
        title.text = o.GetField("name").str;
        this.index.text = index.ToString();
        image.sprite = Resources.Load<Sprite>(o.GetField("image").str);

        //set btn Event
        GetComponent<SetUpButtonEvent>().SetEvent(o);

    }
}
