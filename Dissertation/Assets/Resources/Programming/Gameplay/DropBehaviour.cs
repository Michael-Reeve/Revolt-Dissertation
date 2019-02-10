using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestBehaviour", menuName = "Inventory/Behaviour/DropBehaviour", order = 0)]
public class DropBehaviour : ItemBehaviour
{
	public GameObject visualisation;
	protected GameObject dropObject = null;

	public override void Use(Controller controller, Item item)
	{
		if(dropObject == null)
		{
			dropObject = Instantiate(visualisation);
			dropObject.GetComponent<GridLock>().origin = controller.possessed.gameObject;
			dropObject.GetComponent<GridLock>().offset = new Vector3(0, 0, 3);
		}
		else
		{
			Destroy(dropObject);
			controller.inventory.RemoveItem(controller.inventory.items[controller.inventory.GUI.highlightedItem], new Vector3(dropObject.transform.position.x, dropObject.transform.position.y, dropObject.transform.position.z));
		}
	}
}
