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
			linkedElevator.ToggleDoors();
			Invoke("Teleport", 3f);
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
		linkedElevator.ToggleDoors();
		ToggleDoors();
	}

	public void ToggleDoors()
	{
		foreach(Move door in doors)
		{
			door.Toggle();
		}
	}
}
