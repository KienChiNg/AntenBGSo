using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentNavigator : MonoBehaviour
{
    public Transform groupContent;
    public Transform groupTab;
    public Transform groupModel;
    public Transform groupAnimationController;

    //
    public GameObject prefabTabButton;
    public Button nextBtn;
    public Button prevBtn;
    [HideInInspector]
    public int currentContent = 0;
    public Scrollbar scrollBar;
    public RotateModelWithMouse rotateModelScript;
    public Camera camNguyenLy;
    public GameObject zoomSlider;

    // Start is called before the first frame update
    void Start()
    {
        //add event listener
        prevBtn.onClick.AddListener(() =>
        {
            currentContent -= 1;
            ContentBtn(currentContent);
        });

        nextBtn.onClick.AddListener(() =>
        {
            currentContent += 1;
            ContentBtn(currentContent);
        });
    }

    private void Update()
    {

        //disable prev btn if current question = 0
        if (currentContent == 0)
        {
            prevBtn.interactable = false;
        }
        else
        {
            prevBtn.interactable = true;
        }

        //disable next btn if current question = length
        if (currentContent == groupContent.childCount - 1)
        {
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    public void SetupContent(List<JSONObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int index = i;
            //
            if (list[i].HasField("model"))
            {
                GameObject model = Resources.Load<GameObject>(LessonReference._3DModel + list[i].GetField("model").str);
                GameObject initModel = Instantiate(model, groupModel);
                //setup model, content
                initModel.SetActive(false);

                if (list[i].HasField("animation_controller"))
                {
                    AnimationControllerAttach aca = initModel.AddComponent<AnimationControllerAttach>();

                    //load animation controller
                    GameObject ac = Resources.Load<GameObject>(LessonReference._2DAnimationController + list[i].GetField("animation_controller").str);
                    Debug.Log(LessonReference._2DAnimationController + list[i].GetField("animation_controller").str);
                    //init animation controller
                    GameObject initAC = Instantiate(ac, groupAnimationController);

                    //setup ac
                    initAC.SetActive(false);
                    initAC.GetComponent<AnimationController>().references = initModel.GetComponent<ModelPrefabRef>();
                    aca.animationController = initAC;

                }
            }
            else if (list[i].HasField("image"))
            {
                GameObject image = Resources.Load<GameObject>(LessonReference._2DImage + list[i].GetField("image").str);
                GameObject initimage = Instantiate(image, groupModel);

                //setup image
                initimage.GetComponent<SpritePrefabRef>().scrollBar = zoomSlider;
                initimage.GetComponent<Canvas>().worldCamera = camNguyenLy;
                initimage.SetActive(false);
            }

            //init content
            GameObject content = Resources.Load<GameObject>(LessonReference._2DContent + list[i].GetField("content").str);
            // Debug.Log(LessonReference._2DContent + list[i].GetField("content").str);
            GameObject initContent = Instantiate(content, groupContent);
            initContent.SetActive(false);


            //init button   
            // GameObject button = Instantiate(prefabTabButton, groupTab);
            // button.SetActive(true);
            // button.GetComponentInChildren<Text>().text = (index + 1) + ". " + list[i].GetField("name").str;
            //add button event

            // button.GetComponent<Button>().onClick.AddListener(delegate
            // {
            //     ContentBtn(index);
            // });
        }

        ContentBtn(0);

        if (list.Count <= 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void ContentBtn(int index)
    {
        rotateModelScript.resetView3D();
        currentContent = index;
        for (int i = 0; i < groupContent.childCount; i++)
        {
            groupContent.GetChild(i).gameObject.SetActive(false);
            groupModel.GetChild(i + 1).gameObject.SetActive(false);
        }
        groupContent.GetChild(index).gameObject.SetActive(true);
        groupModel.GetChild(index + 1).gameObject.SetActive(true);
        //update scroll bar
        float scrollVal = (1f / groupContent.childCount) * (index + 1);

        if (scrollVal > scrollBar.size)
        {
            scrollBar.value = (scrollVal - scrollBar.size) / (1 - scrollBar.size);
        }
        else
        {
            scrollBar.value = 0;
        }
    }
}
