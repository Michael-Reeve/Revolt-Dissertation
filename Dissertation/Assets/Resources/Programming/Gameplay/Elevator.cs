using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour 
{
	public Elevator linkedElevator;
	public Character player;
	public List<Move> doors;

	public void TeleportPlayer()
	{
		if(player != null && linkedElevator != null)
		{
			ToggleDoors();
			Invoke("Teleport", 1f);
			linkedElevator.ToggleDoors();
		}
		else
		{
			Debug.Log("No values set!" + this.name);
		}
	}

	public void Teleport()
	{
		Vector3 playerOffset = player.transform.position - transform.position;
		player.transform.position = linkedElevator.transform.position + playerOffset;
	}

	public void ToggleDoors()
	{
		foreach(Move door in doors)
		{
			door.Toggle();
		}
	}
}
