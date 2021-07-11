using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{

	private AudioSource audio;
	public AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
       audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMusicControl()
	{
    	if (audio.isPlaying == false) {
       	 	audio.Play();
    	}
    	else
    	{
        	audio.Pause();
    	}
    }

}
