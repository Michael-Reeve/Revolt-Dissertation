using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour 
{

	public Image icon;

	public void LoadIcon()
	{
		StartCoroutine(SpinIcon());
	}

	private IEnumerator SpinIcon()
    {
		while (GameManager.instance.loadingLevel) 
		{
			icon.rectTransform.Rotate(new Vector3(0, 0, icon.rectTransform.eulerAngles.z + 0.3f));
            yield return null;
        }
    }
}
