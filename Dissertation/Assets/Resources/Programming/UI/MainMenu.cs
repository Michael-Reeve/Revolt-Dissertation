using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	public LoadScreen loadScreen;
	public Options options;
	public AudioSource audioSource;

	void Start()
	{
		//SaveGame.Load();
		if(options)
			options.Load();
	}

	public void ToggleActive()
	{
		this.gameObject.SetActive(!this.gameObject.activeInHierarchy);
		if(this.gameObject.activeInHierarchy && Input_Manager.cursorLocked == true)
		{
			Input_Manager.LockCursor(false);
		}
		else if(this.gameObject.activeInHierarchy == false)
		{
			Input_Manager.LockCursor(true);
		}
		EventManager.TriggerEvent("DisableInput");
	}

	public void LoadLevel(string levelName)
	{
		GameManager.instance.LoadLevel(levelName);
		if(loadScreen)
			loadScreen.LoadIcon();
	}

	public void ExitGame()
	{
		if(GameManager.instance.playingGame)
		{
			loadScreen.gameObject.SetActive(true);
			LoadLevel("MainMenu");
		}
		else
			Application.Quit();
	}

	public void PlaySound(AudioClip clip)
	{
		if(audioSource != null)
		{
		audioSource.clip = clip;
		audioSource.Play();
		}
	}

	public void DeleteAllSaves()
	{
		PlayerPrefs.DeleteAll();
		JSON.Delete("playerinfo.txt");
	}

	public void DeleteSave(string saveKey)
	{
		PlayerPrefs.DeleteKey(saveKey);
	}
}
