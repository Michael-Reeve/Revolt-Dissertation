using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour 
{
	private bool active = false;
	public bool inUse = false;
	public Inventory inventoryReference;
	public GameObject slotUI;
	public List<GameObject> slots = new List<GameObject>();
	public GUIDatabase database;
	public int maxHorizontalSize = 4;
	public int highlightedItem;

	public bool Active
	{
		get
		{
			return active;
		}
		set
		{
			if (value == true)
			{
				UpdateList();
				Time.timeScale = 0.1f;
				active = value;
			}
			else
			{
				Time.timeScale = 1f;
				active = value;
			}
		}
	}
	
	public void HighlightItem(float input)
	{
		slots[highlightedItem].GetComponent<UISlot>().selected.enabled = false;
		if(input > 0)
			highlightedItem += 1;
		else if(input < 0)
			highlightedItem -= 1;
		if(highlightedItem > slots.Count - 1)
			highlightedItem = 0;
		else if (highlightedItem < 0)
			highlightedItem = slots.Count - 1;
		EventSystem.current.SetSelectedGameObject(slots[highlightedItem]);
		slots[highlightedItem].GetComponent<UISlot>().selected.enabled = true;
	}

	public void UseHighlighted()
	{
		inventoryReference.UseItem(highlightedItem);
	}

	public void UpdateList()
	{
		ClearList();
		foreach(InventorySlot slot in inventoryReference.items)
		{
			CreateSlot(slot);
		}
		HighlightItem(0);
	}

	protected void ClearList()
	{
		foreach(GameObject slot in slots)
		{		
			slot.GetComponent<UISlot>().Destroy();
		}
		slots.Clear();
	}

	protected void CreateSlot(InventorySlot slot)
	{
		GameObject newSlot = Instantiate(slotUI, Vector2.zero, Quaternion.identity, this.transform);
		UISlot newSlotUI = newSlot.GetComponent<UISlot>();
		if(slot.ContainedItem != null)
		{
			if(slot.ContainedItem.stackable == true)
			{
				newSlotUI.itemQuantity.text = slot.Quantity.ToString();
			}
			else
			{
				newSlotUI.itemQuantity.text = "";
			}
			newSlotUI.itemImage.enabled = true;
			newSlotUI.itemName.text = slot.ContainedItem.itemName;
			newSlotUI.itemDescription.text = slot.ContainedItem.itemDescription;
			newSlotUI.itemImage.sprite = slot.ContainedItem.image;
		}
		else
		{
			newSlotUI.itemQuantity.text = "";
			newSlotUI.itemName.text = "";
			newSlotUI.itemDescription.text = "";
			newSlotUI.itemImage.enabled = false;
		}
		newSlotUI.inventoryUI = this;
		newSlotUI.itemFrame.sprite = database.itemFrame;
		newSlotUI.index = slots.Count;
		newSlot.name = "ItemSlot " + slots.Count;
		newSlotUI.itemDescription.enabled = false;
		slots.Add(newSlot);
	}
	
}
