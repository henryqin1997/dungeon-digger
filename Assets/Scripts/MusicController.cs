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
        Debug.Assert(audioSource != null);
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

    public void PlaySound(AudioClip sound, float volumeScale)
    {
        audioSource.PlayOneShot(sound, volumeScale);
    }

    public void PlaySound(AudioClip sound)
    {
        PlaySound(sound, 1.0f);
    }

    public void OnMusicControl()
	{
    	if (audioSource.isPlaying == false)
        {
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
