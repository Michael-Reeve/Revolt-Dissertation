using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour 
{
	public Text emailButtonText;
	public Email emailData;
	public GameObject emailDoc;
	public Text emailTitle;
	public Text emailContents;

	void Start()
	{
		emailButtonText.text = emailData.emailTitle;
	}

	public void ToggleEmail()
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