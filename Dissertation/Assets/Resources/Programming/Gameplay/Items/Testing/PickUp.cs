using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, Interactible, ISave
{
	public Item item;
	[SerializeField]
	protected Transform transformData;
	[SerializeField]
	protected bool active;

	void Start()
	{
		ObjectPool.instance.usedObjects.Add(this);
	}

	public void Interact(PlayerController controller)
	{
		controller.GetComponent<Inventory>().AddItem(item);
		ObjectPool.instance.AddToPool(this);
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
