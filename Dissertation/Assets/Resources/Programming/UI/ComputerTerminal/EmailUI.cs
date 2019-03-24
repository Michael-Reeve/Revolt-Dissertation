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
		public AudioClip sfxClick; 
		private Button button;

		void Start()
		{
			button = GetComponent<Button>();
			if(emailData)
				emailButtonText.text = emailData.emailTitle;
		}

		public void ToggleEmail()
		{
			PlaySound();
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
		}
		private void PlaySound()
		{
			if(audioSource != null && sfxClick != null)
			{
				audioSource.clip = sfxClick;
				audioSource.Play();
			}
		}
	}
}
