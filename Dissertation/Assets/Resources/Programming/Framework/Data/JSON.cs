using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON
{
	public static string GetProfile(int profile = 1)
	{
		string path = string.Concat(Application.dataPath, "/playerProfiles/" + profile);
		if(Directory.Exists(path) == false)
		{
			Directory.CreateDirectory(path);
		}
		return path;
	}
	public static T Load<T>(string filename, int profile) where T: class
	{
		string path = string.Concat(GetProfile(profile), filename);
		if(File.Exists(path))
		{
			return JsonUtility.FromJson<T>(File.ReadAllText(path));
		}
		return default(T);
	}

	public static void Save<T>(string filename, int profile, T data) where T: class
	{
		string path = string.Concat(GetProfile(profile), filename);
		File.WriteAllText(path, JsonUtility.ToJson(data));
	}

	public static void Delete(string filename, int profile)
	{
		string path = string.Concat(GetProfile(profile), filename);
		File.Delete(path);
	}

	public static bool CheckSave(string filename, int profile)
	{
		string path = string.Concat(GetProfile(profile), filename);
		return File.Exists(path);
	}

}
