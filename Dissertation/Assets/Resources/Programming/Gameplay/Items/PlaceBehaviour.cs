using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestBehaviour", menuName = "Inventory/Behaviour/PlaceBehaviour", order = 0)]
public class PlaceBehaviour : ItemBehaviour
{
	public override void Use(Controller controller, Item item)
    {
        //item.itemObject
    }
}
