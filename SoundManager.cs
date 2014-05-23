using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public AudioClip music1;
	public AudioClip correctSound;
	public AudioClip hitSound;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	public void PlayAudio (AudioClip clip) 
	{
		audio.clip = clip;
		audio.Play();
		if (clip.name == "music1")
			audio.loop = true;
		else
			audio.loop = false;
	}
}
