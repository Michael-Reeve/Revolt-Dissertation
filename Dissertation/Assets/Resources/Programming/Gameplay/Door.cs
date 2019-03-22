﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactible 
{
	public bool active = true;
	[Space]
	public bool locked = false;
	public Item key;
	public Dialogue lockedDialogue;
	[Space]
	public Transform openState;
	protected Transform defaultState;
	protected bool closed = true;
	[Space]
	public AudioSource audioSource;
	public List<AudioClip> openNoise;
	public List<AudioClip> closeNoise;
	public List<AudioClip> lockedNoise;

	void Start()
	{
		defaultState = transform;
	}


	public void Interact(PlayerController controller)
	{
		if(locked)
		{
			PlaySound();
			if(controller.inventory.equippedItem == key)
			{
				Unlock();
			}
			else
			{
				DialogueManager.instance.AddDialogue(lockedDialogue);
			}
		}
		else
		{
			ToggleDoor();
		}
	}

	public bool IsActive()
	{
		return active;
	}

	public void Unlock()
	{
		locked = false;
		ToggleDoor();
	}

	public void PlaySound()
	{
		if(locked)
		{
			audioSource.clip = RandomClip(lockedNoise);
		}
		else if(closed)
		{
			audioSource.clip = RandomClip(openNoise);
		}
		else
		{
			audioSource.clip = RandomClip(closeNoise);
		}
		audioSource.Play();
	}

	public AudioClip RandomClip(List<AudioClip> audioList)
	{
		if(audioList.Count >= 1)
			return audioList[Random.Range(0, audioList.Count - 1)];
		else
			return null;
	}

	public void ToggleDoor()
	{
		PlaySound();
		if(closed)
		{
			StartCoroutine(MoveTo(openState, 0.7f));
			StartCoroutine(RotateTo(openState, 0.7f));
		}
		else
		{
			StartCoroutine(MoveTo(defaultState, 0.5f));
			StartCoroutine(RotateTo(defaultState, 0.5f));
		}
	}

	IEnumerator MoveTo(Transform desiredState, float speed)
	{
		float alpha = 0f;
		while(Vector3.Equals(transform.position, desiredState.position) == false)
		{
			alpha += Time.deltaTime * speed;
			transform.position = Vector3.MoveTowards(transform.position, desiredState.position, alpha);
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator RotateTo(Transform desiredState, float speed)
	{
		float alpha = 0f;
		while(Vector3.Equals(transform.rotation, desiredState.rotation) == false)
		{
			alpha += Time.deltaTime * speed;
			transform.rotation = Quaternion.Lerp(transform.rotation, desiredState.rotation, alpha);
			yield return new WaitForEndOfFrame();
		}
	}
}