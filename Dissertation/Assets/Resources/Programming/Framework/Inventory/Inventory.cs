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
		if(GUI)
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
				DropItem(item, controller.possessed.transform.position + controller.possessed.transform.forward);
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

	public void RemoveItem(InventorySlot item, Vector3 worldPosition)
	{
		if(items.Contains(item) && item.ContainedItem != null)
		{
			if(controller && item.ContainedItem.itemObject != null)
			{
				DropItem(item, worldPosition);
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

	public void DropItem(InventorySlot item, Vector3 worldPosition)
	{
		if(ObjectPool.instance)
		{
			if(ObjectPool.instance.ContainsItem(item.ContainedItem))
			{
				ObjectPool.instance.Spawn(worldPosition, item.ContainedItem.itemObject.transform.rotation, ObjectPool.instance.FindItemIndex(item.ContainedItem));
			}
			else
			{
				Instantiate(item.ContainedItem.itemObject, controller.possessed.transform.position + controller.possessed.transform.forward, item.ContainedItem.itemObject.transform.rotation);
			}
		}
		if(item.ContainedItem.eventTrigger != "")
		{
			EventManager.TriggerEvent(item.ContainedItem.eventTrigger);
		}
	}

	public void Save()
	{
		JSON.Save<InventoryContainer>("playerinfo.txt", GameManager.saveProfile, new InventoryContainer(items));
		Debug.Log(JsonUtility.ToJson(new InventoryContainer(items)));
	}

	public void Load()
	{	
		if(JSON.CheckSave("playerinfo.txt", GameManager.saveProfile))
			items = JSON.Load<InventoryContainer>("playerinfo.txt", GameManager.saveProfile).items;
		UpdateUI();
	}
}
