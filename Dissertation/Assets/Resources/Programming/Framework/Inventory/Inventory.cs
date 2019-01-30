﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	public int inventorySize;
	public List<InventorySlot> items = new List<InventorySlot>();
	public Equippable equippedItem;
	public InventoryUI GUI;
	public Controller controller;

	void Start()
	{
		for(int i = 0; i < inventorySize; i++)
		{
			items.Add(new InventorySlot());
		}
	}

	public void ToggleActive()
	{
		if(GUI.inUse == false)
		{
			GUI.gameObject.SetActive(!GUI.gameObject.activeInHierarchy);
			GUI.Active = !GUI.Active;
			if(GUI.Active && Input_Manager.cursorLocked == true)
			{
				Input_Manager.LockCursor(false);
			}
			else if(GUI.Active == false)
			{
				Input_Manager.LockCursor(true);
			}
		}
	}

	public void AddItem(Item newItem)
	{
		foreach(InventorySlot item in items)
		{
			if(newItem == item.ContainedItem && item.ContainedItem.stackable == true)
			{
				item.Quantity += 1;
				break;
			}
			else if(item.ContainedItem == null)
			{
				item.ContainedItem = newItem;
				item.Quantity = 1;
				break;
			}
		}
	}

	public void RemoveItem(InventorySlot item)
	{
		if(controller && item.ContainedItem.itemObject != null)
		{
			Instantiate(item.ContainedItem.itemObject, controller.possessed.transform.position + controller.possessed.transform.forward, item.ContainedItem.itemObject.transform.rotation);
		}
		if(item.ContainedItem.eventTrigger != "")
		{
			Debug.Log("Event Triggered: " + item.ContainedItem.eventTrigger);
			EventManager.TriggerEvent(item.ContainedItem.eventTrigger);
		}
		if(item.Quantity > 1)
		{
			item.Quantity -= 1;
		}
		else
		{
			item.ContainedItem = null;
		}
	}

	public void SwapItems(int itemIndex, int swapIndex)
	{
		items = Utility.Swap<InventorySlot>(items.ToArray(), itemIndex, swapIndex).ToList();
		GUI.UpdateList();
	}

	public void UseItem(int itemIndex)
	{
		if(items[itemIndex].ContainedItem is Useable)
		{
			Useable useItem = (Useable)items[itemIndex].ContainedItem;
			useItem.Use(controller);
		}
		else if (items[itemIndex].ContainedItem is Equippable)
		{
			Equippable useItem = (Equippable)items[itemIndex].ContainedItem;
			useItem.Equip(controller);
		}
	}
}
