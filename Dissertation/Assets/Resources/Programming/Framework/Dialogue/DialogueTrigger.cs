using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
	public Dialogue[] dialogues;
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.GetComponent<Character>())
		{
			foreach(Dialogue dialogue in dialogues)
			{
				DialogueManager.instance.AddDialogue(dialogue);
			}
			Destroy(this.gameObject);
		}
	}
}
