using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{
	public bool active;
	public Character possessed;
	protected int layer = 9;
	public Inventory inventory;
	public Camera activeCamera;
	public void Possess(Character newCharacter)
	{
		if(possessed != null)
		{
			possessed.gameObject.layer = 0;
		}
		possessed = newCharacter;
		possessed.gameObject.layer = layer;
	}
}
