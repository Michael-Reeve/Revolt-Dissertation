using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speakers : MonoBehaviour 
{

	public AudioClip defaultNoise;
	public List<AudioSource> speakers;

	// Use this for initialization
	void Start () 
	{
		ResumeNoise();
	}

	private bool AssignAudio(AudioSource audioSource, AudioClip clip, bool loop = false, float volume = 1)
	{
		if(clip == null || audioSource == null)
			return false;
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.volume = volume;
		return true;
	}

	private void PlayClipVolume(AudioClip clip, float volume)
	{
		foreach(AudioSource speaker in speakers)
		{
			AssignAudio(speaker, clip, false, volume);
			speaker.Play();
		}
		StartCoroutine(WaitUntilClipFinished(defaultNoise));
	}

	public void PlayClip(AudioClip clip)
	{
		foreach(AudioSource speaker in speakers)
		{
			AssignAudio(speaker, clip, false, 0.6f);
			speaker.Play();
		}
		StartCoroutine(WaitUntilClipFinished(defaultNoise));
	}

	private IEnumerator WaitUntilClipFinished(AudioClip clip, float volume = 1)
	{
		while(speakers[0].isPlaying)
		{
			foreach(AudioSource speaker in speakers)
			{
				if(speaker.loop == true)
					Debug.LogError("Speaker is looping!");
				if(speaker.isPlaying)
					continue;
			}
			yield return new WaitForEndOfFrame();
		}
		if (clip == defaultNoise)
			ResumeNoise();
		else
			PlayClipVolume(clip, volume);
	}

	private void ResumeNoise()
	{
		foreach(AudioSource speaker in speakers)
		{
			AssignAudio(speaker, defaultNoise, true, 0.2f);
			speaker.Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
