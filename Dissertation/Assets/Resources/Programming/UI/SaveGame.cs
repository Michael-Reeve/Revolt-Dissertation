using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveGame 
{
	public static void Save()
	{
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects(rootObjects);
		List<ISave> saves = new List<ISave>();
		foreach (GameObject gameObject in rootObjects)
		{
			saves.AddRange(Utility.GetInterface<ISave>(gameObject.GetComponentsInChildren(typeof(ISave), true)));
		}
		for(int i = 0; i < saves.Count; i++)
		{
			saves[i].Save();
			Debug.Log("Object: " + i + " out of " + saves.Count);
		}
	}

	public static void Load()
	{
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects(rootObjects);
		List<ISave> saves = new List<ISave>();
		foreach (GameObject gameObject in rootObjects)
		{
			saves.AddRange(Utility.GetInterface<ISave>(gameObject.GetComponentsInChildren(typeof(ISave), true)));
		}
		for(int i = 0; i < saves.Count; i++)
		{
			saves[i].Load();
			Debug.Log("Object: " + i + " out of " + saves.Count);
		}
	}
	
	public static void SaveVector3(Vector3 position, string key)
	{
		PlayerPrefs.SetFloat(key + "x", position.x);
		PlayerPrefs.SetFloat(key + "y", position.y);
		PlayerPrefs.SetFloat(key + "z", position.z);
	}

	public static Vector3 LoadVector3(string key)
	{
		return new Vector3(PlayerPrefs.GetFloat(key + "x"), PlayerPrefs.GetFloat(key + "y"), PlayerPrefs.GetFloat(key + "z"));
	}

	public static void SaveQuaternion(Quaternion rotation, string key)
	{
		PlayerPrefs.SetFloat(key + "x", rotation.x);
		PlayerPrefs.SetFloat(key + "y", rotation.y);
		PlayerPrefs.SetFloat(key + "z", rotation.z);
		PlayerPrefs.SetFloat(key + "w", rotation.w);
	}

	public static Quaternion LoadQuaternion(string key)
	{
		return new Quaternion(PlayerPrefs.GetFloat(key + "x"), PlayerPrefs.GetFloat(key + "y"), PlayerPrefs.GetFloat(key + "z"), PlayerPrefs.GetFloat(key + "w"));
	}
}
