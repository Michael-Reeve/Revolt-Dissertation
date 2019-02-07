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

	void Awake()
	{

	}

	public void Interact(PlayerController controller)
	{
		controller.GetComponent<Inventory>().AddItem(item);
		this.gameObject.SetActive(false);
	}

	public void Save()
	{
		transformData = transform;
		active = this.gameObject.activeInHierarchy;
		JSON.SaveAppend("leveldata.txt", this);
	}

	public void Load()
	{
		//JSON.Load<GameObject>("leveldata.txt");
	}
}
