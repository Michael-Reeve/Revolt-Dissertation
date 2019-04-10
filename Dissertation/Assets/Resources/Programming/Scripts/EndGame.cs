using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndGame : MonoBehaviour 
{
	public Dialogue lastLine;
	public UnityEvent lastEvents;
	public Image blackScreenPanel;
	public bool hasPlayed;

	public void End()
	{
		if(DialogueManager.instance)
		{
			StartCoroutine(WaitForDialogue());
		}
		else
			Debug.Log("No Dialogue Manager! WTF");
	}

	IEnumerator WaitForDialogue()
	{
		while(DialogueManager.instance.currentDialogue)
		{
			yield return new WaitForEndOfFrame();
		}
		EndSequence();
	}

	public void EndSequence()
	{
		lastEvents.Invoke();
		Invoke("FadeToBlack", 2f);
	}

	public void FadeToBlack()
	{
		StartCoroutine(Lerp(blackScreenPanel, Color.black, 0.005f));
		DialogueManager.instance.AddDialogue(lastLine);
	}

	IEnumerator Lerp(Image image, Color desiredColor, float speed = 0.01f)
	{
		float alpha = 0f;
		while(image.color != desiredColor)
		{
			alpha += speed;
			image.color = Color.Lerp(image.color, desiredColor, alpha);
			yield return 0;
		}
		Application.Quit();
	}
}
