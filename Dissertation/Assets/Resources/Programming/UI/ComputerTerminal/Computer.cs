﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Computer
{
	public class Computer : MonoBehaviour, Interactible
	{
		public bool locked;
		public string keycode;
		public List<Email> emails = new List<Email>();
		public GraphicRaycaster graphicRaycaster;
		public EventSystem eventSystem;
		public GameObject emailList;
		public GameObject emailUI;
		public GameObject emailDoc;
		public LockScreen lockScreen;
		public Text emailTitle;
		public Text emailContents;
		public AudioSource computerAudio;

		void Awake()
		{
			for(int i = 0; i < emails.Count; i++)
			{
				GameObject newEmail = Instantiate(emailUI, emailList.transform);
				EmailUI newEmailUI = newEmail.GetComponent<EmailUI>();
				newEmailUI.emailData = emails[i];
				newEmailUI.emailDoc = emailDoc;
				newEmailUI.emailTitle = emailTitle;
				newEmailUI.emailContents = emailContents;
				if(computerAudio)
					newEmailUI.audioSource = computerAudio;
			}
			if(locked)
			{
				lockScreen.gameObject.SetActive(true);
				lockScreen.GenerateKeys(keycode);
			}
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
					Button resultButton = result.gameObject.GetComponent<Button>();
					if(locked)
					{
						if(result.gameObject.GetComponent<EmailUI>() == false)
						{
							resultButton.OnPointerClick(pointerEventData);
						}
					}
					else
					{
						resultButton.OnPointerClick(pointerEventData);
					}
					//result.gameObject.GetComponent<Button>().interactable = false;
				}
			}
			if(locked)
			{
				if(lockScreen.getKeyCode() == keycode)
				{
					lockScreen.gameObject.SetActive(false);
					locked = false;
				}
			}
		}

		public Button GetButton()
		{
			PointerEventData pointerEventData = new PointerEventData(eventSystem);
			pointerEventData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			graphicRaycaster.Raycast(pointerEventData, results);
			
			foreach(RaycastResult result in results)
			{
				if(result.gameObject.GetComponent<Button>())
				{
					return result.gameObject.GetComponent<Button>();
				}
			}
			return default(Button);
		}

		public bool IsActive()
		{
			return true;
		}

		public void ToggleActive(GameObject gameObject)
		{
			gameObject.SetActive(!gameObject.activeInHierarchy);
		}
	}
}