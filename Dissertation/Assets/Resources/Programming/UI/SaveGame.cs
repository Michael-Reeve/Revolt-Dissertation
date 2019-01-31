using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGame 
{
	public static void SavePlayerPosition(Vector3 playerPosition)
	{
		PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
		PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
		PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);
	}

	public static Vector3 LoadPlayerPosition(Vector3 playerPosition)
	{
		return new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
	}
}
