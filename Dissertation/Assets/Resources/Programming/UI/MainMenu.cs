﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	public LoadScreen loadScreen;

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
		AsyncOperation async = SceneManager.LoadSceneAsync(levelName);
		if(loadScreen)
			loadScreen.LoadIcon(async);
	}
}
