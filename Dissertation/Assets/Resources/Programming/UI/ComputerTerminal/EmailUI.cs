using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Computer
{
	public class EmailUI : MonoBehaviour
	{
		public Text emailButtonText;
		public Email emailData;
		public GameObject emailDoc;
		public Text emailTitle;
		public Text emailContents;
		public AudioSource audioSource;
		private Button button;

		void Start()
		{
			button = GetComponent<Button>();
			if(emailData)
				emailButtonText.text = emailData.emailTitle;
		}

		public void ToggleEmail()
		{
			if(emailData != null)
			{
				if(emailTitle.text != emailData.emailTitle)
				{
					if(emailDoc.activeInHierarchy == false)
					{
						emailDoc.SetActive(true);
					}
					emailTitle.text = emailData.emailTitle;
					emailContents.text = emailData.emailContents;
				}
				else
				{
					if(emailDoc.activeInHierarchy == false)
						emailDoc.SetActive(true);
					else
						emailDoc.SetActive(false);
				}
			}
			if(audioSource)
				audioSource.Play();
		}
	}
}
