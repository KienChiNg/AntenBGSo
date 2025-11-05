using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customRenderQueue : MonoBehaviour
{
    public int RenderQueue=3000;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<Material>();
        mat.renderQueue = RenderQueue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
