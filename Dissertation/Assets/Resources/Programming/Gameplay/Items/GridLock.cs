using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLock : MonoBehaviour 
{
	public GameObject origin;
	public Vector3 offset;
	private Vector3 currentPosition;
	void Update () 
	{
		transform.position = origin.transform.position + (origin.transform.forward * offset.z);
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
	}
}
