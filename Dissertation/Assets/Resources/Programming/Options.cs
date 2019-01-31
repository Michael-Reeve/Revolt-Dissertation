using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour 
{
	public AudioMixer masterMixer;
	public Slider mainVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;
	public Slider dialogueVolumeSlider;
	void Awake()
	{
		mainVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume", 0.8f);
		musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
		sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
		dialogueVolumeSlider.value = PlayerPrefs.GetFloat("DialogueVolume", 0.8f);
	}

	public void SetMainVolume()
	{
		masterMixer.SetFloat("mainVolume", Mathf.Log10(mainVolumeSlider.value) * 20);
	}

	public void SetMusicVolume()
	{
		masterMixer.SetFloat("musicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
	}

	public void SetSFXVolume()
	{
		masterMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolumeSlider.value) * 20);
	}

	public void SetDialogueVolume()
	{
		masterMixer.SetFloat("dialogueVolume", Mathf.Log10(dialogueVolumeSlider.value) * 20);
	}
	
	void OnApplicationQuit()
	{
		Debug.Log("Test");
		PlayerPrefs.SetFloat("MainVolume", mainVolumeSlider.value);
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
		PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
		PlayerPrefs.SetFloat("DialogueVolume", dialogueVolumeSlider.value);
	}
}
