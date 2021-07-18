using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
	private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }

    public void PlaySound(AudioClip sound, float volumeScale = 1f)
    {
        audioSource.PlayOneShot(sound, volumeScale);
    }

    public void OnMusicControl()
	{
    	if (audioSource.isPlaying == false) {
       	 	audioSource.Play();
    	}
    	else
    	{
        	audioSource.Pause();
    	}
    }

    public void OnConsumableConsumed(IConsumable consumable)
    {
        PlaySound(consumable.GetConsumeAudioClip(), 2f);
    }


}
