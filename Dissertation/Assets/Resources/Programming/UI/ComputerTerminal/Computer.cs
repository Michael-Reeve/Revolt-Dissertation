using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Computer : MonoBehaviour, Interactible
{
	public bool locked;
	public int[] keycode;
	public List<Email> emails = new List<Email>();
	public Vector2 cursorPosition;
	public RectTransform cursor;
	public GraphicRaycaster graphicRaycaster;
	public EventSystem eventSystem;
	public GameObject emailList;
	public GameObject emailUI;
	public GameObject emailDoc;
	public Text emailTitle;
	public Text emailContents;

	void Awake()
	{
		for(int i = 0; i < emails.Count; i++)
		{
			GameObject newEmail = Instantiate(emailUI, emailList.transform);
			EmailUI newEmailUI = newEmail.GetComponent<EmailUI>();
			Debug.Log(emails[i].emailTitle);
			Debug.Log(newEmailUI);
			newEmailUI.emailData = emails[i];
			newEmailUI.emailDoc = emailDoc;
			newEmailUI.emailTitle = emailTitle;
			newEmailUI.emailContents = emailContents;
		}
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
				result.gameObject.GetComponent<Button>().Select();
				result.gameObject.GetComponent<Button>().OnPointerClick(pointerEventData);
				//result.gameObject.GetComponent<Button>().interactable = false;
			}
		}
	}

	public void ToggleActive(GameObject gameObject)
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}

	public void SetCursorPosition()
	{

	}

	public void LockOn()
	{

	}

}
