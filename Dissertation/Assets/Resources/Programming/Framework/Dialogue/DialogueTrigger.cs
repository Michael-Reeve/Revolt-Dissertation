using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
	public Dialogue dialogue;
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.GetComponent<Character>())
		{
			DialogueManager.instance.AddDialogue(dialogue);
			Destroy(this.gameObject);
		}
	}
}
