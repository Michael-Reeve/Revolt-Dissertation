using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON
{
	//////
	///	Returns the path to the current player save folder.
	/////
	public static string GetProfile(int profile = 1)
	{
		string path = string.Concat(Application.dataPath, "/playerProfiles/" + profile + "/");
		if(Directory.Exists(path) == false)
		{
			Directory.CreateDirectory(path);
		}
		return path;
	}

	//////
	///	Loads a generic type from Json and returns it.
	/////
	public static T Load<T>(string filename, int profile) where T: class
	{
		string path = string.Concat(GetProfile(profile), filename);
		if(File.Exists(path))
		{
			return JsonUtility.FromJson<T>(File.ReadAllText(path));
		}
		return default(T);
	}

	//////
	///	Saves usage statistics - data is passed in and saved on a new line in a text document.
	/////
	public static void SaveUsage(int profile, string data)
	{
		string path = string.Concat(GetProfile(profile), "UsageStats.txt");
		if(File.Exists(path))
		{
			File.AppendAllText(path, data + System.Environment.NewLine);
		}
		else
		{
			File.WriteAllText(path, data + System.Environment.NewLine);
		}
	}

	//////
	///	Saves a generic type to the file specified.
	/////
	public static void Save<T>(string filename, int profile, T data) where T: class
	{
		string path = string.Concat(GetProfile(profile), filename);
		File.WriteAllText(path, JsonUtility.ToJson(data));
	}

	//////
	///	Deletes the file (of filename) in the save profile specified.
	/////
	public static void Delete(string filename, int profile)
	{
		string path = string.Concat(GetProfile(profile), filename);
		File.Delete(path);
	}

	//////
	///	Checks if a file exists.
	//////
	public static bool CheckSave(string filename, int profile)
	{
		string path = string.Concat(GetProfile(profile), filename);
		return File.Exists(path);
	}
}
