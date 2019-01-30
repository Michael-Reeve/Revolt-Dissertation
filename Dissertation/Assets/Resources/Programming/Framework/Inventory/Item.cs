using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : ScriptableObject 
{
	public string itemName;
	public string itemDescription;
	public Sprite image;
	public GameObject itemObject;
	public bool stackable;
	public string eventTrigger;
}
