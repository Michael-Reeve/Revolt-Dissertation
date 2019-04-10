using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour, ISave
{
	public bool hasPlayed;
	[Header("References")]
	public Dialogue[] dialogues;
	public GameObject blackScreen;
	public Image blackScreenPanel;
	public Image logo;
	public List<GameObject> GUIObjects = new List<GameObject>();
	public AudioSource audioSource;
	public AudioClip backgroundSFX;
	[Header("Timing")]
	public float introLength = 30;


	private string uniqueID;

	public void AddDialogue()
	{
		foreach(Dialogue dialogue in dialogues)
		{
			if(dialogue)
				DialogueManager.instance.AddDialogue(dialogue);
		}
	}

	public void ToggleGUIElements(bool active)
	{
		foreach(GameObject guiObject in GUIObjects)
		{
			guiObject.SetActive(active);
		}
	}

	public void EndIntroduction()
	{
		hasPlayed = true;
		blackScreen.SetActive(false);
		ToggleGUIElements(true);
		EventManager.TriggerEvent("DisableInput");
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(12f);
		StartCoroutine(Lerp(logo, Color.white));
		yield return new WaitForSeconds(6f);
		StartCoroutine(Lerp(logo, Color.clear));
		yield return new WaitForSeconds(introLength - 18f);
		StartCoroutine(FadeIn());
		yield return new WaitForSeconds(1);
		EndIntroduction();
	}

	IEnumerator FadeIn()
	{
		float alpha = 1f;
		while(!hasPlayed)
		{
			alpha -= 0.03f;
			blackScreenPanel.color = new Color(blackScreenPanel.color.r, blackScreenPanel.color.g, blackScreenPanel.color.b, alpha);
			yield return 0;
		}
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
	}

	#region SaveData
	public void LoadData(ObjectData objectData)
	{
		hasPlayed = objectData.active;
	}

	public void Save()
	{
		GameManager.instance.AddLevelData(uniqueID, new ObjectData(hasPlayed, transform.position, transform.rotation, transform.parent));
	}

	public void Load()
	{
		if(uniqueID == null)
			uniqueID = GetUniqueID();
		if(GameManager.instance.levelDictionary != null)
		{
			ObjectData loadData = new ObjectData();
			Debug.Log(gameObject.name + " | " + GameManager.instance.levelDictionary.ContainsKey(uniqueID));
			GameManager.instance.levelDictionary.TryGetValue(uniqueID, out loadData);
			LoadData(loadData);
			Debug.Log("Loading Data for " + this.name);
		}
		if(hasPlayed == false)
		{
			EventManager.TriggerEvent("DisableInput");
			blackScreen.SetActive(true);
			ToggleGUIElements(false);
			AddDialogue();
			StartCoroutine(Wait());
			if(backgroundSFX)
				audioSource.PlayOneShot(backgroundSFX);
		}
	}

	public string GetUniqueID()
	{
		return this.gameObject.name;
	}
	#endregion
}
