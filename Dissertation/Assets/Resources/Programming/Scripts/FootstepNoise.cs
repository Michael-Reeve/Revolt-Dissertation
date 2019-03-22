using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepNoise : MonoBehaviour 
{

	public AudioSource audioSource;
	public Headbob headbob;
	[Range (10,-10)]
	public int offsetTrigger;
	public List<AudioClip> footsteps;
	private bool stepAvailable;
	
	void Update () 
	{
		Debug.Log(headbob.offsetAlpha / 10);
		if(Mathf.RoundToInt(headbob.offsetAlpha / 10) == offsetTrigger)
		{
			stepAvailable = false;
			audioSource.clip = footsteps[(int)Random.Range(0, footsteps.Count)];
			audioSource.Play();
		}
		if(-Mathf.RoundToInt(headbob.offsetAlpha / 10) == offsetTrigger)
		{
			stepAvailable = true;
		}
	}
}
