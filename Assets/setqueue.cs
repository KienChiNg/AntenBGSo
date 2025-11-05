using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setqueue : MonoBehaviour
{
    public GameObject[] list;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < list.Length; i ++)
        {
            list[i].GetComponent<MeshRenderer>().material.renderQueue = 4001;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
