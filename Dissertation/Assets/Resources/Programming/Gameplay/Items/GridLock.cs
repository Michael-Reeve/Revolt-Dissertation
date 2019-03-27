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
	public bool overlapping = true;
	public LayerMask ignoreLayers;
	private Color originalColor;
	private MeshRenderer meshRend;
	void Awake()
	{
		meshRend = GetComponentInChildren<MeshRenderer>();
		originalColor = meshRend.material.color;
		SetOverlapColor(overlapping);
	}

	void Update () 
	{
		if(controller != null)
		{
			if(controller.inventory.GUI.highlightedItem != highlight)
			{
				Destroy(this.gameObject);
			}
		}

		transform.position = origin.transform.position + (origin.transform.forward * offset.z);
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

		Collider[] collidersArray = new Collider[2];
		Physics.OverlapSphereNonAlloc(transform.position, 0.1f, collidersArray, ignoreLayers, QueryTriggerInteraction.Collide);
		if(collidersArray[0] != null && collidersArray[1] == null)
		{
			Debug.Log(collidersArray[0]);
			overlapping = false;
		}
		else
		{
			Debug.Log(collidersArray[0] + " | " + collidersArray[1]);
			overlapping = true;
		}
		SetOverlapColor(overlapping);
	}

	public void SetOverlapColor(bool cantBePlaced)
	{
		if(cantBePlaced && meshRend.material.color != Color.red)
		{
			meshRend.material.color = Color.red;
			meshRend.material.SetColor("_EmissionColor", originalColor + new Color(1, 0, 0, 0));
		}
		else if (!cantBePlaced && meshRend.material.color != Color.blue)
		{
			meshRend.material.color = Color.blue;
			meshRend.material.SetColor("_EmissionColor", originalColor + new Color(0, 0, 1, 0));
		}
	}

}
