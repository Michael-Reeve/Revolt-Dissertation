using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGame 
{
	public static void SavePlayerPosition(Vector3 playerPosition)
	{
		PlayerPrefs.SetFloat("PlayerPX", playerPosition.x);
		PlayerPrefs.SetFloat("PlayerPY", playerPosition.y);
		PlayerPrefs.SetFloat("PlayerPZ", playerPosition.z);
	}

	public static Vector3 LoadPlayerPosition()
	{
		return new Vector3(PlayerPrefs.GetFloat("PlayerPX"), PlayerPrefs.GetFloat("PlayerPY"), PlayerPrefs.GetFloat("PlayerPZ"));
	}

	public static void SavePlayerRotation(Vector3 playerRotation)
	{
		PlayerPrefs.SetFloat("PlayerRX", playerRotation.x);
		PlayerPrefs.SetFloat("PlayerRY", playerRotation.y);
		PlayerPrefs.SetFloat("PlayerRZ", playerRotation.z);
	}

	public static Vector3 LoadPlayerRotation()
	{
		return new Vector3(PlayerPrefs.GetFloat("PlayerRX"), PlayerPrefs.GetFloat("PlayerRY"), PlayerPrefs.GetFloat("PlayerRZ"));
	}
}
