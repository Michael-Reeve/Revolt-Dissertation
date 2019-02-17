using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equippable Item", menuName = "Inventory/Equippable", order = 0)]
public class Equippable : Item 
{
    public Sprite cursor;
	public void Equip(Controller controller)
    {
        if(controller.inventory.equippedItem == null)
        {
            Debug.Log("Equip! " + this.itemName + " to " + controller.name);
            controller.inventory.equippedItem = this;
        }
        else if(controller.inventory.equippedItem == this)
        {
            Debug.Log("Unequip! " + this.itemName + " from " + controller.name);
            controller.inventory.equippedItem = null;
        }
    }
}
