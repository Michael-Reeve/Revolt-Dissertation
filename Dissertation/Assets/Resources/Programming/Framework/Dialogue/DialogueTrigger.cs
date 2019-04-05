using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour, ISave
{
	public Dialogue[] dialogues;
	public UnityEvent events;
	private string uniqueID;

	void Awake()
	{
		uniqueID = GetUniqueID();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.GetComponent<Character>())
		{
			foreach(Dialogue dialogue in dialogues)
			{
				DialogueManager.instance.AddDialogue(dialogue);
			}
			events.Invoke();
			this.gameObject.SetActive(false);
		}
	}

	public void LoadData(ObjectData objectData)
	{
		if(objectData.position == Vector3.zero)
			return;
		this.gameObject.SetActive(objectData.active);
		transform.position = objectData.position;
		transform.rotation = objectData.rotation;
		transform.parent = objectData.parent;
	}

	public void Save()
	{
		GameManager.instance.AddLevelData(uniqueID, new ObjectData(gameObject.activeInHierarchy, transform.position, transform.rotation, transform.parent));
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
	}

	public string GetUniqueID()
	{
		return string.Format(this.gameObject.name + "{0}" + "{1}" + "{2}", this.transform.position, this.transform.rotation, this.transform.parent);
	}
}
