using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public bool playingGame;
	public bool loadingLevel;
	public string currentLevel;
    private int level = 3;
	public Dictionary<string, ObjectData> levelDictionary = new Dictionary<string, ObjectData>();

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	public AsyncOperation LoadLevel(string levelName)
	{
		SaveGame.Save();
		JSON.Save(SceneManager.GetActiveScene().name + ".txt", new LevelContainer(levelDictionary));
		Debug.Log("LEVEL: " + JsonUtility.ToJson(new LevelContainer(levelDictionary)));
		AsyncOperation async = SceneManager.LoadSceneAsync(levelName);
		loadingLevel = true;
		StartCoroutine(WaitForLoad(async, levelName));
		return async;
	}

	public void AddLevelData(string keyObject, ObjectData objectData)
	{
		levelDictionary[keyObject] = objectData;
		//levelDictionary.Add(keyObject, objectData);
	}

	public void LoadLevelData(LevelContainer copyData)
	{
		Debug.Log(copyData.keys.Count);
		levelDictionary.Clear();
		for(int i = 0; i < copyData.keys.Count; i++)
		{
			Debug.Log(copyData.values[i].position);
			levelDictionary.Add(copyData.keys[i], copyData.values[i]);
		}
	}

	protected IEnumerator WaitForLoad(AsyncOperation async, string levelName)
    {
		while (!async.isDone) 
		{
			Debug.Log(levelName + " loading: " + async.progress);
            yield return null;
        }
		Debug.Log(levelName + " loading finished.");
		loadingLevel = false;
		SceneLoaded(levelName);
		SaveGame.Load();
    }

	protected void SceneLoaded(string levelName)
	{
		currentLevel = SceneManager.GetActiveScene().name;
		Debug.Log(levelDictionary.Count);
		if(JSON.CheckSave(SceneManager.GetActiveScene().name +".txt"))
			LoadLevelData(JSON.Load<LevelContainer>(SceneManager.GetActiveScene().name + ".txt"));
		Debug.Log(levelDictionary.Count);
		if(levelName != "MainMenu")
			playingGame = true;
		else
			playingGame = false;
	}
	
}
public class LevelContainer
{
	public List<string> keys = new List<string>();
	public List<ObjectData> values = new List<ObjectData>();
	public LevelContainer(Dictionary<string, ObjectData> _levelDictionary)
	{
		foreach(string key in _levelDictionary.Keys)
		{
			keys.Add(key);
		}
		foreach(ObjectData value in _levelDictionary.Values)
		{
			values.Add(value);
		}
	}
}
