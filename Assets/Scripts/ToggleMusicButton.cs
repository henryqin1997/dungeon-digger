using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusicButton : MonoBehaviour
{	
	public Sprite sprite_soundon;
	public Sprite sprite_soundoff;

	private bool ischange = false;
    // Start is called before the first frame update
    void Start()
    {
     	transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        ischange = !ischange;
        if (ischange)
        {
            transform.GetComponent<Image>().sprite = sprite_soundoff;
        }
        else
        {
            transform.GetComponent<Image>().sprite = sprite_soundon;
        }
    }
}
