using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour 
{
	public Dialogue[] dialogues;
	public UnityEvent events;
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.GetComponent<Character>())
		{
			foreach(Dialogue dialogue in dialogues)
			{
				DialogueManager.instance.AddDialogue(dialogue);
			}
			events.Invoke();
			Destroy(this.gameObject);
		}
	}
}
