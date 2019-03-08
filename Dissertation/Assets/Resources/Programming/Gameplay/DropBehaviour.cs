using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestBehaviour", menuName = "Inventory/Behaviour/DropBehaviour", order = 0)]
public class DropBehaviour : ItemBehaviour
{
	public GameObject visualisation;
	protected GameObject dropObject = null;
	protected GridLock dropObjectInfo;

	public override void Use(Controller controller, Item item)
	{
		if(dropObject == null)
		{
			dropObject = Instantiate(visualisation);
			dropObjectInfo = dropObject.GetComponent<GridLock>();
			dropObjectInfo.origin = controller.possessed.gameObject;
			dropObjectInfo.offset = new Vector3(0, 0, 3);
			dropObjectInfo.controller = controller;
			dropObjectInfo.highlight = controller.inventory.GUI.highlightedItem;
		}
		else
		{
			if(dropObjectInfo.overlapping == false)
			{
				Destroy(dropObject);
				controller.inventory.RemoveItem(controller.inventory.items[controller.inventory.GUI.highlightedItem], new Vector3(dropObject.transform.position.x, dropObject.transform.position.y, dropObject.transform.position.z));
			}
			else
			{
				Destroy(dropObject);
			}
		}
	}
}
