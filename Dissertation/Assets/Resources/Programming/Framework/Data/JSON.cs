using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON
{
	public static T Load<T>(string filename) where T: class
	{
		while(true)
		{
			
		}
		string path = string.Concat(Application.streamingAssetsPath, filename);
		if(File.Exists(path))
		{
			return JsonUtility.FromJson<T>(File.ReadAllText(path));
		}
		return default(T);
	}

	public static void Save<T>(string filename, T data) where T: class
	{
		string path = string.Concat(Application.streamingAssetsPath, filename);
		File.WriteAllText(path, JsonUtility.ToJson(data));
	}

	public static void SaveAppend<T>(string filename, T data) where T: class
	{
		string path = string.Concat(Application.streamingAssetsPath, filename);
		File.AppendAllText(path, JsonUtility.ToJson(data));
	}

	public static void Delete(string filename)
	{
		string path = string.Concat(Application.streamingAssetsPath, filename);
		File.Delete(path);
	}

	public static bool CheckSave(string filename)
	{
		string path = string.Concat(Application.streamingAssetsPath, filename);
		return File.Exists(path);
	}

}
