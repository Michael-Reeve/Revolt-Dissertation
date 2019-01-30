using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public InventoryUI inventoryUI;
	public bool hovered;
	public int index;
	public Text itemName, itemDescription, itemQuantity;
	public Image itemImage, itemFrame;
	public Vector2 position;

	public void OnDrag(PointerEventData eventData)
	{ 
		if(itemName.text != "")
		{
			inventoryUI.inUse = true;
			itemImage.transform.position = Input.mousePosition; itemName.transform.position = Input.mousePosition;
			SetParent(itemImage, this.transform.parent); SetParent(itemName, this.transform.parent);
			//itemImage.transform.parent = this.transform.parent; itemName.transform.parent = this.transform.parent;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(itemName.text != "")
		{
			inventoryUI.inUse = false;

			PointerEventData pointerEventData = new PointerEventData(transform.parent.GetComponent<EventSystem>());
			pointerEventData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			transform.parent.GetComponent<GraphicRaycaster>().Raycast(pointerEventData, results);
			Debug.Log(results.Count);
			if(results.Count == 0)
			{
				inventoryUI.inventoryReference.RemoveItem(inventoryUI.inventoryReference.items[index]);
				inventoryUI.UpdateList();
				return;
			}
			foreach (RaycastResult result in results)
			{
				if(result.gameObject.GetComponent<UISlot>() == true && result.gameObject != this.gameObject)
				{
					inventoryUI.inventoryReference.SwapItems(index, result.gameObject.GetComponent<UISlot>().index);
				}
				else
				{
					itemImage.transform.position = position; itemName.transform.position = position;
					SetParent(itemImage, this.transform); SetParent(itemName, this.transform);
					//itemImage.transform.parent = this.transform; itemName.transform.parent = this.transform;
				}
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
		if(eventData.hovered.Contains(itemName.gameObject) || eventData.hovered.Contains(itemQuantity.gameObject))
		{
			if(hovered != true)
			{
				hovered = true;
				itemDescription.transform.parent = this.transform.parent;
				Invoke("DisplayText", 1 * Time.timeScale);
			}
		}
    }

	public void OnPointerExit(PointerEventData eventData)
    {
		if(eventData.hovered.Contains(itemName.gameObject) == false && eventData.hovered.Contains(itemQuantity.gameObject) == false)
		{
			hovered = false;
			itemDescription.transform.parent = this.transform;
			DisplayText();
		}
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		if(itemName.text != "" && hovered == true)
		{
			inventoryUI.inventoryReference.UseItem(index);
		}
	}

	public void DisplayText()
	{
		if(hovered == true)
		{
			itemDescription.enabled = true;
		}
		else
		{
			itemDescription.enabled = false;
		}
	}

	public void Destroy()
	{
		Destroy(itemName.gameObject);
		Destroy(itemImage.gameObject);
		Destroy(itemQuantity.gameObject);
		Destroy(itemDescription.gameObject);
		Destroy(this.gameObject);
	}

	private void SetParent(Graphic element, Transform parent)
	{
		element.transform.parent = parent;
	}

	
}
