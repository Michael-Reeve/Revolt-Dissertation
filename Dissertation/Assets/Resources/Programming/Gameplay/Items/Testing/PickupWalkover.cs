using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWalkover : MonoBehaviour, Interactible
{
	public Item item;
	// Use this for initialization

	public void Interact(PlayerController controller)
	{
		controller.GetComponent<Inventory>().AddItem(item);
		Destroy(this.gameObject);
	}

	public bool IsActive()
	{
		return true;
	}
}
