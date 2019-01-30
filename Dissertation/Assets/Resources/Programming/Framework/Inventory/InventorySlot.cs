using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
	public bool active;
	protected Item containedItem;
	protected int quantity = 1;

	public Item ContainedItem
	{
		get
		{
			return containedItem;
		}
		set
		{
			if (value == containedItem)
			{
				return;
			}
			else if (value == null)
			{
				containedItem = null;
				Quantity = 0;
			}
			else
			{
				containedItem = value;
			}
		}
	}

	public int Quantity 
	{
		get
		{
			return quantity;
		}
		set
		{
			if(value < 0)
				quantity = 0;
			else
				quantity = value;
		}
	} 

}
