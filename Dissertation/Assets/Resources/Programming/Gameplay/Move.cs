﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour, ISave
{
	private bool moved = false;
	private bool moving = false;
	public Vector3 offset;
	public float speed;
	[Space]
	public AudioSource audioSource;
	public AudioClip moveSound;
	public AudioClip finishSound;
	private Vector3 startPosition;
	private Vector3 desiredPosition;
	private string uniqueID;

	void Awake()
	{
		startPosition = transform.position;
		desiredPosition = startPosition + offset;
		//speed /= 100;
		uniqueID = GetUniqueID();
	}

	public void Toggle()
	{
		if(moving)
			return;
		if(moved)
			Reverse();
		else
			GoTo();
	}

	public void GoTo()
	{
		StartCoroutine(MoveTo(desiredPosition));
		moving = true;
		moved = true;
		if(audioSource != null)
		{
			audioSource.clip = moveSound;
			audioSource.Play();
		}
			
	}

	public void Reverse()
	{
		if(moved)
		{
			StartCoroutine(MoveTo(startPosition));
			moving = true;
			moved = false;
			if(audioSource != null)
			{
				audioSource.clip = moveSound;
				audioSource.loop = true;
				audioSource.Play();
			}
		}
	}

	IEnumerator MoveTo(Vector3 newPos)
	{
		float alpha = 0f;
		while(Vector3.Equals(transform.position, newPos) == false)
		{
			alpha = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, newPos, alpha);
			yield return 0;
		}
		Finished();
	}

	private void Finished()
	{
		moving = false;
		if(audioSource != null)
		{
			audioSource.loop = false;
			audioSource.clip = finishSound;
			audioSource.Play();
		}
	}

	public void LoadData(ObjectData objectData)
	{
		if(objectData.position == Vector3.zero)
			return;
		moved = objectData.active;
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
