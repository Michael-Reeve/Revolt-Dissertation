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
	protected float mainVolume, musicVolume, sfxVolume, dialogueVolume;

	public void LoadVolume()
	{
		mainVolume = PlayerPrefs.GetFloat("MainVolume", 0.8f);
		SetMainVolume();
		musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
		SetMusicVolume();
		sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
		SetSFXVolume();
		dialogueVolume = PlayerPrefs.GetFloat("DialogueVolume", 0.8f);
		SetDialogueVolume();
		SetSliders();
	}

	private void SetSliders()
	{
		mainVolumeSlider.value = mainVolume;
		musicVolumeSlider.value = musicVolume;
		sfxVolumeSlider.value = sfxVolume;
		dialogueVolumeSlider.value = dialogueVolume;
	}

	public void SetMainVolume()
	{
		mainVolume = Mathf.Log10(mainVolumeSlider.value) * 20;
		masterMixer.SetFloat("mainVolume", mainVolume);
	}

	public void SetMusicVolume()
	{
		musicVolume = Mathf.Log10(musicVolumeSlider.value) * 20;
		masterMixer.SetFloat("musicVolume", musicVolume);
	}

	public void SetSFXVolume()
	{
		sfxVolume = Mathf.Log10(sfxVolumeSlider.value) * 20;
		masterMixer.SetFloat("sfxVolume", sfxVolume);
	}

	public void SetDialogueVolume()
	{
		dialogueVolume = Mathf.Log10(dialogueVolumeSlider.value) * 20;
		masterMixer.SetFloat("dialogueVolume", dialogueVolume);
	}
	
	void OnApplicationQuit()
	{
		PlayerPrefs.SetFloat("MainVolume", mainVolumeSlider.value);
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
		PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
		PlayerPrefs.SetFloat("DialogueVolume", dialogueVolumeSlider.value);
	}
}
