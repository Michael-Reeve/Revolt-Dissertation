using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLock : MonoBehaviour 
{
	public GameObject origin;
	public Controller controller;
	public int highlight;
	public Vector3 offset;
	private Vector3 currentPosition;
	public bool overlapping;
	private Color originalColor;
	void Awake()
	{
		MeshRenderer meshRend = GetComponentInChildren<MeshRenderer>();
		originalColor = meshRend.material.color;
		meshRend.material.color = Color.blue;
		meshRend.material.SetColor("_EmissionColor", originalColor + new Color(0, 0, 1, 0));
	}

	void Update () 
	{
		transform.position = origin.transform.position + (origin.transform.forward * offset.z);
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
		if(controller != null)
		{
			if(controller.inventory.GUI.highlightedItem != highlight)
			{
				Destroy(this.gameObject);
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.layer == this.gameObject.layer)
		{
			MeshRenderer meshRend = GetComponentInChildren<MeshRenderer>();
			meshRend.material.color = Color.red;
			meshRend.material.SetColor("_EmissionColor", originalColor + new Color(1, 0, 0, 0));
			overlapping = true;
		}
    }
	void OnTriggerExit(Collider other)
    {
		if(other.gameObject.layer == this.gameObject.layer)
		{
			MeshRenderer meshRend = GetComponentInChildren<MeshRenderer>();
			meshRend.material.color = Color.blue;
			meshRend.material.SetColor("_EmissionColor", originalColor + new Color(0, 0, 1, 0));
			overlapping = false;
		}
    }
}
