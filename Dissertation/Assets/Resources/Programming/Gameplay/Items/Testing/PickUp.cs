using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour, Interactible, ISave
{
	public Item item;
	[SerializeField]
	protected Transform transformData;
	[SerializeField]
	public bool active = true;
	public bool repeatEvents = false;
	private bool pickedUp;
	public UnityEvent pickupEvent;
	private string uniqueID;

	void Start()
	{
		ObjectPool.instance.usedObjects.Add(this);
		uniqueID = GetUniqueID();
	}

	public void Interact(PlayerController controller)
	{
		if(active)
		{
			controller.GetComponent<Inventory>().AddItem(item);
			if(!pickedUp)
				pickupEvent.Invoke();
			ObjectPool.instance.AddToPool(this);

			if(repeatEvents)
			{
				pickedUp = false;
			}
			else
			{
				pickedUp = true;
			}
		}
	}

	public bool IsActive()
	{
		return active;
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
		GameManager.instance.AddLevelData(uniqueID, new ObjectData(gameObject.activeInHierarchy, transform.position, transform.rotation, transform.parent));
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

	public string GetUniqueID()
	{
		return string.Format(this.gameObject.name + "{0}" + "{1}" + "{2}", this.transform.position, this.transform.rotation, this.transform.parent.name);
	}
}
