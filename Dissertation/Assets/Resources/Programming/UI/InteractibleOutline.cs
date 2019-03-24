using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleOutline : MonoBehaviour 
{

	public PlayerController player;
	public Interactible interactible;
	public MeshRenderer mesh;
	private Color defaultColor;

	void Start()
	{
		defaultColor = mesh.materials[1].GetColor("_OutlineColor") + new Color(0, 0, 0, 2);
		mesh.materials[1].SetColor("_OutlineColor", Color.clear);
	}

	void LateUpdate()
	{
		if(player)
		{
			if(player.targettedInteractible.Contains(interactible))
			{
				mesh.materials[1].SetColor("_OutlineColor", defaultColor);
			}
			else
			{
				mesh.materials[1].SetColor("_OutlineColor", Color.clear);
			}
		}
	}
}
