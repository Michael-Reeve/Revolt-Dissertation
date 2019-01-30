using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	public void UpdateList()
	{
		ClearList();
		foreach(InventorySlot slot in inventoryReference.items)
		{
			CreateSlot(slot);
		}
		PositionSlots(10f);
		//ScaleSlots();
	}

	protected void ClearList()
	{
		foreach(GameObject slot in slots)
		{		
			slot.GetComponent<UISlot>().Destroy();
		}
		slots.Clear();
	}

	// Position Slots!
	// Padding is used for both X and Y.
	// 
	protected void PositionSlots(float padding = 1f)
	{
		Vector2 screenXY = new Vector2(Screen.width, Screen.height);
		//Debug.Log(screenXY);
		for(int i = 0; i < slots.Count; i++)
		{
			Vector2 slotPosition = new Vector2(screenXY.x / 3, screenXY.y / 1.5f);
			if(i >= maxHorizontalSize)
			{
				slotPosition.y -= (100f * (i / maxHorizontalSize) + (padding * (i / maxHorizontalSize)));
			}
			if(i != 0)
			{
				slotPosition.x += 100 * (i - (maxHorizontalSize * (i / maxHorizontalSize))) + (padding * (i - (maxHorizontalSize * (i / maxHorizontalSize))));
			}
			slots[i].transform.position = slotPosition;
			slots[i].GetComponent<UISlot>().position = slotPosition;
		}
	}

	protected void ScaleSlots()
	{
		Vector2 screenXY = new Vector2(Screen.width, Screen.height);
		Vector2 slotScale = new Vector2(screenXY.x / 1920 , screenXY.y / 1080);
		Debug.Log(slotScale);
		for(int i = 0; i < slots.Count; i++)
		{
			slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(slots[i].transform.localScale.x * slotScale.x, slots[i].transform.localScale.y * slotScale.y);
		}
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
		} else
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
