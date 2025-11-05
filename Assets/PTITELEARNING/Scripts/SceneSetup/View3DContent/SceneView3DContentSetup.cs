using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LessonReference;

public class SceneView3DContentSetup : MonoBehaviour
{
    public enum DetailRouter
    {
        DetailRouter1,
        DetailRouter2,
    }
    public Text title;
    public Image background;
    public ContentNavigator contentNavigator;
    public DetailRouter detailRouter;

    // Start is called before the first frame update
    void Start()
    {
        //Setup UI
        // SetUpScene(GetUIReference());
        switch (detailRouter)
        {
            case DetailRouter.DetailRouter1:
                SetUpScene(Resources.Load<TextAsset>("TruyenSongAnten" + "/VDCTS/TLSKGTD").text);
                break;
            case DetailRouter.DetailRouter2:
                SetUpScene(Resources.Load<TextAsset>("TruyenSongAnten" + "/TLSCN/TLTGHNTTT").text);
                break;
        }
    }

    public string GetUIReference()
    {
        string subURL = "";

        if (Router.instance.subLesson != "")
        {
            subURL += "/" + Router.instance.subLesson;
        }

        if (Router.instance.details != "")
        {
            subURL += "/" + Router.instance.details;
        }

        switch (Router.instance.currentLesson)
        {
            case Lesson._CauKienDienTu:
                TextAsset ckdt = Resources.Load<TextAsset>("CauKienDienTu" + subURL);
                return ckdt.text;
            case Lesson._TruyenSongAnten:
                TextAsset _tsaten = Resources.Load<TextAsset>("TruyenSongAnten" + subURL);
                return _tsaten.text;
            case Lesson._ViXuLy:
                TextAsset _vlsi = Resources.Load<TextAsset>("ViXuLy" + subURL);
                return _vlsi.text;
        }

        return "";
    }

    public void SetUpScene(string UIref)
    {
        if (UIref != "")
        {
            JSONObject json = new JSONObject(UIref);

            //set title
            // title.text = json.GetField("title").str;

            //set background
            // background.sprite = Resources.Load<Sprite>(json.GetField("background").str);

            //set content
            contentNavigator.SetupContent(json.GetField("list_sub_lesson").list);
        }
    }


}
