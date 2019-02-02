using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveGame 
{
	public static void Save(Transform parent)
	{
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects( rootObjects );
		
		List<ISave> saves = Utility.GetInterface<ISave>(parent.GetComponentsInChildren(typeof(ISave), true));
		for(int i = 0; i < saves.Count; i++)
		{
			saves[i].Save();
			Debug.Log("Object: " + i + " out of " + saves.Count);
		}
	}

	public static void Load(Transform parent)
	{
		List<ISave> saves = Utility.GetInterface<ISave>(parent.GetComponentsInChildren(typeof(ISave), true));
		for(int i = 0; i < saves.Count; i++)
		{
			saves[i].Load();
			Debug.Log("Object: " + i + " out of " + saves.Count);
		}
	}
	
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

	public static void SavePlayerRotation(Quaternion playerRotation)
	{
		PlayerPrefs.SetFloat("PlayerRX", playerRotation.x);
		PlayerPrefs.SetFloat("PlayerRY", playerRotation.y);
		PlayerPrefs.SetFloat("PlayerRZ", playerRotation.z);
		PlayerPrefs.SetFloat("PlayerRW", playerRotation.w);
	}

	public static Quaternion LoadPlayerRotation()
	{
		return new Quaternion(PlayerPrefs.GetFloat("PlayerRX"), PlayerPrefs.GetFloat("PlayerRY"), PlayerPrefs.GetFloat("PlayerRZ"), PlayerPrefs.GetFloat("PlayerRW"));
	}
}
