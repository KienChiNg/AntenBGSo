using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePrefabRef : MonoBehaviour
{
    public GameObject scrollBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(scrollBar != null)
        {
            scrollBar.SetActive(false);
        }
       
    }

    private void OnDisable()
    {
        if(scrollBar != null)
        {
            scrollBar.SetActive(true);
        }
        
    }
}
