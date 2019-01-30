using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour 
{

	private UnityAction impact;
	void OnEnable()
	{
		impact = new UnityAction (Impact);
		EventManager.StartListening("PlayerCollision", impact);
	}

	void Impact()
	{
		StartCoroutine(Shake(5f, 1f));
	}

	IEnumerator Shake (float duration, float magnitude)
	{
		Vector3 originalPos = transform.position;

		float elapsed = 0f;

		while(elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			transform.position = new Vector3(x, y, originalPos.z);

			elapsed += Time.deltaTime;
			
			yield return null;
		}

		transform.position = originalPos;
	}
}
