using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
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
		StartCoroutine(WaitForLoad(async));
		return async;
	}

	protected IEnumerator WaitForLoad(AsyncOperation async)
    {
		while (!async.isDone) 
		{
			//Debug.Log(async.progress);
            yield return null;
        }
		loadingLevel = false;
		SaveGame.Load();
    }
}
