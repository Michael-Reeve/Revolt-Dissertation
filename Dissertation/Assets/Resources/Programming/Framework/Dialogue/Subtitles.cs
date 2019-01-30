using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Subtitles : MonoBehaviour 
{
	Text subtitleText;
	DialogueManager subtitleData;
	private UnityAction subtitleChange;

	void OnEnable()
	{
		EventManager.StartListening("DialogueChange", subtitleChange);
	}

	void OnDisable()
	{
		EventManager.StopListening("DialogueChange", subtitleChange);
	}

	void Awake()
	{
		subtitleText = GetComponent<Text>();
		subtitleChange = new UnityAction (SetSubtitle);
	}

	void SetSubtitle()
	{
		if(DialogueManager.instance.currentDialogue != null)
		{
			subtitleText.text = DialogueManager.instance.currentDialogue.subtitle;
		}
		else
		{
			subtitleText.text = "";
		}
	}
}
