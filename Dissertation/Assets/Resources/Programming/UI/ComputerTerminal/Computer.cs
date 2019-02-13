using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Computer : MonoBehaviour, Interactible
{
	public bool lockedOn;
	public Vector2 cursorPosition;
	public RectTransform cursor;
	public GraphicRaycaster graphicRaycaster;
	public EventSystem eventSystem;

	void Awake()
	{
		//graphicRaycaster = GetComponent<GraphicRaycaster>();
	}

	public void Interact(PlayerController controller)
	{
		PointerEventData pointerEventData = new PointerEventData(eventSystem);
		pointerEventData.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();
		graphicRaycaster.Raycast(pointerEventData, results);
		foreach(RaycastResult result in results)
		{
			if(result.gameObject.GetComponent<Button>())
			{
				result.gameObject.GetComponent<Button>().OnPointerClick(pointerEventData);
			}
		}
	}

	public void SetCursorPosition()
	{

	}

	public void LockOn()
	{

	}

}
