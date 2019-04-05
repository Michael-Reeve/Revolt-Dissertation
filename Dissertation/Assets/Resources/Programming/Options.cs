using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour, ISave
{
	public AudioMixer masterMixer;
	public Slider mainVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;
	public Slider dialogueVolumeSlider;
	protected float mainVolume, musicVolume, sfxVolume, dialogueVolume;

	public float Convert(float conversion)
	{
		return Mathf.Log10(conversion) * 20;
	}

	public void Load()
	{
		mainVolume = PlayerPrefs.GetFloat("MainVolume", 0.8f);
		masterMixer.SetFloat("mainVolume", Convert(mainVolume));
		musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
		masterMixer.SetFloat("musicVolume", Convert(musicVolume));
		sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
		masterMixer.SetFloat("sfxVolume", Convert(sfxVolume));
		dialogueVolume = PlayerPrefs.GetFloat("DialogueVolume", 0.8f);
		masterMixer.SetFloat("dialogueVolume", Convert(dialogueVolume));
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
		mainVolume = mainVolumeSlider.value;
		masterMixer.SetFloat("mainVolume", Convert(mainVolume));
	}

	public void SetMusicVolume()
	{
		musicVolume = musicVolumeSlider.value;
		masterMixer.SetFloat("musicVolume", Convert(musicVolume));
	}

	public void SetSFXVolume()
	{
		sfxVolume = sfxVolumeSlider.value;
		masterMixer.SetFloat("sfxVolume", Convert(sfxVolume));
	}

	public void SetDialogueVolume()
	{
		dialogueVolume = dialogueVolumeSlider.value;
		masterMixer.SetFloat("dialogueVolume", Convert(dialogueVolume));
	}
	
	void OnApplicationQuit()
	{
		Save();
	}

	public void Save()
	{
		PlayerPrefs.SetFloat("MainVolume", mainVolumeSlider.value);
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
		PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
		PlayerPrefs.SetFloat("DialogueVolume", dialogueVolumeSlider.value);
		PlayerPrefs.Save();
	}

	public string GetUniqueID()
	{
		return "options";
	}

	public void SetQuality()
	{
		//QualitySettings.SetQualityLevel();
	}
}
