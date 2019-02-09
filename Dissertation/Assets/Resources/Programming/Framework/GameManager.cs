using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public bool playingGame;
	public bool loadingLevel;
    private int level = 3;

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
		AsyncOperation async = SceneManager.LoadSceneAsync(levelName);
		loadingLevel = true;
		StartCoroutine(WaitForLoad(async, levelName));
		return async;
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
		if(levelName != "MainMenu")
			playingGame = true;
		else
			playingGame = false;
	}
	
}
