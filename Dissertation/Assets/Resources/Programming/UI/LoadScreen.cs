using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour 
{

	public Image icon;

	public void LoadIcon(AsyncOperation async)
	{
		StartCoroutine(SpinIcon(async));
	}

	private IEnumerator SpinIcon(AsyncOperation async)
    {
		while (!async.isDone) 
		{
			icon.rectTransform.Rotate(new Vector3(icon.rectTransform.eulerAngles.z + 0.1f, 0, 0));
            yield return null;
        }
    }
}
