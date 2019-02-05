using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class InventoryContainer
{
	public List<InventorySlot> items = new List<InventorySlot>();
	public InventoryContainer(List<InventorySlot> data)
	{
		items = data;
	}
}

public class Inventory : MonoBehaviour, ISave
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
		UpdateUI();
	}

	public void UpdateUI()
	{
		GUI.UpdateList();
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
		UpdateUI();
	}

	public void RemoveItem(InventorySlot item)
	{
		if(items.Contains(item) && item.ContainedItem != null)
		{
			if(controller && item.ContainedItem.itemObject != null)
			{
				Instantiate(item.ContainedItem.itemObject, controller.possessed.transform.position + controller.possessed.transform.forward, item.ContainedItem.itemObject.transform.rotation);
			}
			if(item.ContainedItem.eventTrigger != "")
			{
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
			UpdateUI();
		}
		else
		{
			Debug.Log("Inventory does not contain the item specified!");
		}
	}

	public void SwapItems(int itemIndex, int swapIndex)
	{
		items = Utility.Swap<InventorySlot>(items.ToArray(), itemIndex, swapIndex).ToList();
		UpdateUI();
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
		UpdateUI();
	}

	public void Save()
	{
		JSONSerialization.Save<InventoryContainer>("playerinfo.txt", new InventoryContainer(items));
		Debug.Log(JsonUtility.ToJson(new InventoryContainer(items)));
	}

	public void Load()
	{	
		if(JSONSerialization.CheckSave("playerinfo.txt"))
			items = JSONSerialization.Load<InventoryContainer>("playerinfo.txt").items;
		UpdateUI();
	}
}
