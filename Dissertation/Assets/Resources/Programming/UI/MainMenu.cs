using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public GameObject[] tabs;
	public List<Button> saveGames = new List<Button>();
	public Button playGameButton;
	public LoadScreen loadScreen;
	public Options options;
	public AudioSource audioSource;
	private bool websiteOpened = false;

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
		Debug.Log("Loading Level: " + levelName);
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

	public void ForceQuit()
	{
		SaveGame.Save();
		Application.Quit();
	}

	public void LoadWebsite(string url)
	{
		Application.OpenURL(url);
	}

	public void LoadWebsiteOnce(string url)
	{
		if(websiteOpened == false)
		{
			Application.OpenURL(url);
			websiteOpened = true;
		}
	}

	public void PlaySound(AudioClip clip)
	{
		if(audioSource != null)
		{
			audioSource.clip = clip;
			audioSource.Play();
		}
	}
	
	public void DisableTabs()
	{
		foreach(GameObject tab in tabs)
		{
			tab.SetActive(false);
		}
	}

	public void ToggleSaveGames(bool interactible)
	{
		foreach(Button saveGame in saveGames)
		{
			saveGame.interactable = interactible;
		}
	}

	public void SetProfile(int profile)
	{
		GameManager.saveProfile = profile;
		if(profile != 0)
			playGameButton.interactable = true;
	}

	public void DeleteAllSaves()
	{
		PlayerPrefs.DeleteAll();
		JSON.Delete("playerinfo.txt", GameManager.saveProfile);
	}
}
