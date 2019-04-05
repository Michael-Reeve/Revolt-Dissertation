using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveProfile : MonoBehaviour 
{
	public MainMenu menu;
	public int profileNumber = 1;
	private Button button;

	void Start()
	{
		button = GetComponent<Button>();
		menu.saveGames.Add(button);
		button.GetComponentInChildren<Text>().text = "Game " + profileNumber;
	}

	public void OnClick()
	{
		menu.ToggleSaveGames(true);
		button.interactable = false;
		menu.SetProfile(profileNumber);
	}
}
