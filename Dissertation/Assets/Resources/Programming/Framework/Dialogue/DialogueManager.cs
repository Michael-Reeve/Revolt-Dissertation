﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour 
{
	public List<Dialogue> dialogueQueue;
	public Dialogue currentDialogue;
	public AudioSource audioSource;

	private static DialogueManager dialogueManager;
    public static DialogueManager instance
    {
        get
        {
            if (!dialogueManager)
            {
                dialogueManager = FindObjectOfType (typeof (DialogueManager)) as DialogueManager;

                if (!dialogueManager)
                {
                    Debug.LogError ("There needs to be one active DialogueManger script on a GameObject in your scene.");
                }
            }

            return dialogueManager;
        }
    }

	public void Pause()
	{
		if(audioSource.isPlaying)
		{
			dialogueQueue.Insert(0, currentDialogue);
			audioSource.Pause();
		}
	}

	private void Play(Dialogue dialogue)
	{
		currentDialogue = dialogue;
		audioSource.clip = dialogue.audio;
		Invoke("Finished", dialogue.audio.length + 0.05f);
		audioSource.Play();
	}

	public void AddDialogue(Dialogue dialogue)
	{
		//Debug.Log(dialogue.dialoguePriority + " Play!");
		if(audioSource.clip != null && audioSource.isPlaying)
		{
			Interrupt(dialogue);
		}
		else
		{
			Play(dialogue);
		}
		SubtitleChange();
	}

	private void Queue(Dialogue dialogue)
	{
		dialogueQueue.Add(dialogue);
	}

	private void Interrupt(Dialogue dialogue)
	{
		switch(dialogue.dialoguePriority)
		{
			case Dialogue.priority.Dialogue:
				Queue(dialogue);
				break;
			case Dialogue.priority.Contextual:
				Pause();
				Play(dialogue);
				break;
		}
	}

	private void Finished()
	{
		//Debug.Log(currentDialogue.name + " Finished");
		if(audioSource.isPlaying == false)
		{
			if(dialogueQueue.Count > 0)
			{
				Play(dialogueQueue[0]);
				dialogueQueue.RemoveAt(0);
			}
			else
			{
				currentDialogue = null;
			}
			SubtitleChange();
		}
	}

	private void SubtitleChange()
	{
		//Debug.Log("Event Triggered: Subtitle Change");
		EventManager.TriggerEvent("DialogueChange");
	}
}