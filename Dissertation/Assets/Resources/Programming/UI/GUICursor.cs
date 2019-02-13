using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICursor : MonoBehaviour 
{
	public Image cursor;
	public GUIDatabase data;
	public PlayerController player;
	void Start()
	{
		SetCursor("default");
	}

	void LateUpdate()
	{
		if(player.targettedInteractible.Count == 0)
		{
			SetCursor("default");
		}
		else if (player.targettedInteractible[0] is Computer)
		{
			SetCursor("cursor");
		}
		else if (player.targettedInteractible[0] is Interactible)
		{
			SetCursor("interactible");
		}
	}
	
	public void SetCursor(string cursorName)
	{
		foreach(namedImage cursorImage in data.cursors)
		{
			if(cursorImage.name.ToLower() == cursorName.ToLower())
			{
				cursor.sprite = cursorImage.image;
				return;
			}
		}
		Debug.Log("No cursor of the name " + cursorName + " found!");
	}
}
