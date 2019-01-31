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

	public static Vector3 LoadPlayerPosition()
	{
		return new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
	}

	public static void SavePlayerRotation(Quaternion playerRotation)
	{
		PlayerPrefs.SetFloat("PlayerQW", playerRotation.w);
		PlayerPrefs.SetFloat("PlayerQX", playerRotation.x);
		PlayerPrefs.SetFloat("PlayerQY", playerRotation.y);
		PlayerPrefs.SetFloat("PlayerQZ", playerRotation.z);
	}

	public static Quaternion LoadPlayerRotation()
	{
		return new Quaternion(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"), PlayerPrefs.GetFloat("PlayerW"));
	}
}
