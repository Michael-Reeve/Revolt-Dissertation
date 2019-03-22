using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepNoise : MonoBehaviour 
{

	public AudioSource audioSource;
	public Headbob headbob;
	[Range (-1, 1)]
	public int offsetTrigger;
	public List<AudioClip> footsteps;
	private bool stepAvailable;
	
	void FixedUpdate () 
	{
		//Debug.Log(headbob.offsetAlpha);
		if(headbob.offsetAlpha * offsetTrigger / 100 >= 1)
		{
			stepAvailable = false;
			audioSource.clip = footsteps[(int)Random.Range(0, footsteps.Count)];
			audioSource.Play();
		}
		if(headbob.offsetAlpha * offsetTrigger / -100 >= 1)
		{
			stepAvailable = true;
		}
	}
}
