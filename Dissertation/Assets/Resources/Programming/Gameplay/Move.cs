using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
	public Vector3 offset;
	public float speed;
	private Vector3 startPosition;
	private Vector3 desiredPosition;

	void Awake()
	{
		startPosition = transform.position;
		desiredPosition = startPosition + offset;

	}

	public void GoTo()
	{
		StartCoroutine("MoveTo");
	}

	IEnumerator MoveTo()
	{
		float alpha = 0f;
		while(Vector3.Equals(transform.position, desiredPosition) == false)
		{
			alpha += 0.01f;
			transform.position = Vector3.Lerp(transform.position, desiredPosition, alpha);
			yield return new WaitForSeconds(speed);
		}
	}

}
