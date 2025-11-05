using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CauTrucHeDKBtn : MonoBehaviour
{
    public string title;
    public GameObject prefab;
    public Text titleText;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            titleText.text = title;
            //delete previous child
            for(int i = 0; i < parent.childCount; i ++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }

            //init
            Instantiate(prefab, parent);
        });
    }
}
