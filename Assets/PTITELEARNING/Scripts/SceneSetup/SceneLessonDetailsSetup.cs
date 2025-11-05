using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LessonReference;

public class SceneLessonDetailsSetup : MonoBehaviour
{
    public Text title;
    public Image icon;
    public Image background;
    public Image image;
    public Transform groupListSubLessonBtn;


    // Start is called before the first frame update
    void Start()
    {
        //Setup UI
        SetUpScene(GetUIReference());
    }

    public string GetUIReference()
    {
        switch (Router.instance.currentLesson)
        {
            case Lesson._CauKienDienTu:
                TextAsset ckdt = Resources.Load<TextAsset>("CauKienDienTu/" + Router.instance.subLesson);
                return ckdt.text;
            case Lesson._TruyenSongAnten:
                TextAsset _tsanten = Resources.Load<TextAsset>("TruyenSongAnten/" + Router.instance.subLesson);
                return _tsanten.text;
            case Lesson._ViXuLy:
                TextAsset _vlsi = Resources.Load<TextAsset>("ViXuLy/" + Router.instance.subLesson);
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
            title.text = json.GetField("title").str;

            //set background
            background.sprite = Resources.Load<Sprite>(json.GetField("background").str);

            //set image
            image.sprite = Resources.Load<Sprite>(json.GetField("image").str);

            //setup button
            SetUpButtonSubLesson(json.GetField("sub_lesson_button_prefab").str, json.GetField("list_sub_lesson").list);
        }
    }

    public void SetUpButtonSubLesson(string button, List<JSONObject> listBtns)
    {
        switch (button)
        {
            case "ButtonSubLessonDetails_1":
                GameObject btn_1 = Resources.Load<GameObject>(_2DButton + "Btn_sub_lesson_details_1");
                for (int i = 0; i < listBtns.Count; i++)
                {
                    GameObject sBtn = Instantiate(btn_1, groupListSubLessonBtn);
                    sBtn.GetComponent<ButtonSubLessonDetails_1>().SetUpComponent(listBtns[i]);
                }
                break;
        }

    }
}
