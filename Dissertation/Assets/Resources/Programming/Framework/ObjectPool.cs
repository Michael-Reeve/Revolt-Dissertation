using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
	public static ObjectPool instance;
	public List<PickUp> pooledObjects;
	public List<PickUp> usedObjects;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	public void AddToPool(PickUp newObject)
	{
		if(usedObjects.Contains(newObject))
			usedObjects.Remove(newObject);
		pooledObjects.Add(newObject);
		newObject.transform.parent = this.transform;
		newObject.transform.position = transform.position;
		newObject.transform.rotation = transform.rotation;
		newObject.transform.localScale = transform.localScale;
		newObject.gameObject.SetActive(false);
	}

	public bool ContainsItem(Item item)
	{
		bool containsItem = false;
		foreach(PickUp pooledObject in pooledObjects)
		{
			if(pooledObject.item == item)
			{
				containsItem = true;
			}
		}
		return containsItem;
	}

	public int FindItemIndex(Item item)
	{
		int itemIndex = 0;
		foreach(PickUp pooledObject in pooledObjects)
		{
			if(pooledObject.item == item)
			{
				itemIndex = pooledObjects.IndexOf(pooledObject);
			}
		}
		return itemIndex;
	}

	public void Spawn(Transform spawnTransform, int index)
	{
		PickUp spawnObject = pooledObjects[index];
		pooledObjects.RemoveAt(index);
		usedObjects.Add(spawnObject);
		spawnObject.transform.parent = null;
		spawnObject.transform.position = spawnTransform.position;
		spawnObject.transform.rotation = spawnTransform.rotation;
		spawnObject.gameObject.SetActive(true);
	}
	
	void Update () 
	{
		
	}
}
