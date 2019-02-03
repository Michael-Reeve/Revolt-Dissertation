using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
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
		StartCoroutine(WaitForLoad(async));
		return async;
	}

	protected static IEnumerator WaitForLoad(AsyncOperation async)
    {
		while (!async.isDone) 
		{
			//Debug.Log(async.progress);
            yield return null;
        }
		SaveGame.Load();
    }
}
