﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICursor : MonoBehaviour 
{
	public Image cursor;
	public GUIDatabase data;
	public PlayerController player;
	Computer.Computer highlightedComputer;

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
		else if (player.targettedInteractible[0] is Computer.Computer && player.targettedInteractible[0].IsActive())
		{
			SetCursor("cursor");
			highlightedComputer = (Computer.Computer)player.targettedInteractible[0];
			if(highlightedComputer.GetButton() != null)
				highlightedComputer.GetButton().Select();
		}
		else if (player.targettedInteractible[0] is Door && player.targettedInteractible[0].IsActive() && player.inventory.equippedItem)
		{
			SetCursor("keycard");
		}
		else if (player.targettedInteractible[0] is Interactible && player.targettedInteractible[0].IsActive())
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
				if(cursorName == "cursor")
					cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(5f, -5f);
				else
					cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				return;
			}
		}
		Debug.Log("No cursor of the name " + cursorName + " found!");
	}
}
