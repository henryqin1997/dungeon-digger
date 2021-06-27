using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingCanvas : MonoBehaviour
{
    Canvas cookingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        cookingCanvas = GetComponent<Canvas> ();
        cookingCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        cookingCanvas.worldCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
