using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
	private bool moved;
	public Vector3 offset;
	public float speed;
	private Vector3 startPosition;
	private Vector3 desiredPosition;

	void Awake()
	{
		startPosition = transform.position;
		desiredPosition = startPosition + offset;
		speed /= 100;
	}

	public void GoTo()
	{
		StartCoroutine("MoveTo");
		moved = true;
	}

	IEnumerator MoveTo()
	{
		float alpha = 0f;
		while(Vector3.Equals(transform.position, desiredPosition) == false)
		{
			alpha += Time.deltaTime * speed;
			transform.position = Vector3.Lerp(transform.position, desiredPosition, alpha);
			yield return new WaitForEndOfFrame();
		}
	}

	public void LoadData(ObjectData objectData)
	{
		if(objectData.position == Vector3.zero)
			return;
		gameObject.SetActive(objectData.active);
		transform.position = objectData.position;
		transform.rotation = objectData.rotation;
		transform.parent = objectData.parent;
	}

	public void Save()
	{
		GameManager.instance.AddLevelData(this.gameObject.name, new ObjectData(gameObject.activeInHierarchy, transform.position, transform.rotation, transform.parent));
	}

	public void Load()
	{
		
		if(GameManager.instance.levelDictionary != null)
		{
			ObjectData loadData = new ObjectData();
			Debug.Log(gameObject.name + " | " + GameManager.instance.levelDictionary.ContainsKey(gameObject.name));
			GameManager.instance.levelDictionary.TryGetValue(this.gameObject.name, out loadData);
			LoadData(loadData);
			Debug.Log("Loading Data for " + this.name);
		}

	}

}
