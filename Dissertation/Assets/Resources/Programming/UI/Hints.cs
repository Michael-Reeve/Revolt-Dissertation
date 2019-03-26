using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour 
{
	public GUIDatabase database;
	[Space]
	public Text hintHeader;
	public Text hintContents;
	private Color headerColor, contentsColor;
	private bool fading = false;
	private bool faded = true;
	public Dictionary<string, string> hintsDictionary = new Dictionary<string, string>();

	void Awake()
	{
		foreach (Hint hint in database.hints)
		{
			hintsDictionary.Add(hint.hintName, hint.hintText);
		}
		headerColor = hintHeader.color;
		contentsColor = hintContents.color;

		ClearText();
	}

	public void ShowHint(string hintName)
	{
		Debug.Log(hintName + " hint requested");
		if(faded)
		{
			FadeHintIn();
			string hint;
			hintsDictionary.TryGetValue(hintName, out hint);
			if(hint != null)
			{
				hintContents.text = hint;
			}
			StartCoroutine(DelayFadeHintOut(6f, hintContents.text));
		}
		else
		{
			ClearText();
			ShowHint(hintName);
		}
	}

	private void ClearText()
	{
		fading = false;
		faded = true;
		hintHeader.color = Color.clear;
		hintContents.color = Color.clear;
	}

	private IEnumerator DelayFadeHintOut(float time, string contents)
	{
		yield return new WaitForSeconds(time);
		if(hintContents.text == contents)
			FadeHintOut();
	}

	private void FadeHintOut()
	{
		if(fading == false)
		{
			fading = true;
			faded = true;
			StartCoroutine(FadeHint(Color.clear, Color.clear));
		}
	}

	private void FadeHintIn()
	{
		if(fading == false)
		{
			fading = true;
			faded = false;
			StartCoroutine(FadeHint(headerColor, contentsColor));
		}
	}

	IEnumerator FadeHint(Color targetColorHeader, Color targetColorContents, float speed = 1)
	{
		float alpha = 0f;
		while(hintHeader.color != targetColorHeader && fading)
		{
			alpha += Time.deltaTime * speed;
			hintHeader.color = Color.Lerp(hintHeader.color, targetColorHeader, alpha);
			hintContents.color = Color.Lerp(hintContents.color, targetColorContents, alpha);
			yield return new WaitForEndOfFrame();
		}
		if(fading)
			fading = false;
	}

	
}
