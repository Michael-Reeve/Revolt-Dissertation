using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepNoise : MonoBehaviour 
{

	public AudioSource audioSource;
	public Headbob headbob;
	[Range (1,-1)]
	public int offsetTrigger;
	public List<AudioClip> footsteps;
	private bool stepAvailable;
	
	void Update () 
	{
		if(Mathf.Round(Mathf.Sin(headbob.velocity.magnitude * Time.time / 3)) == offsetTrigger)
		{
			stepAvailable = false;
			audioSource.clip = footsteps[(int)Random.Range(0, footsteps.Count)];
			audioSource.Play();
		}
		if(Mathf.Round(Mathf.Sin(headbob.velocity.magnitude * Time.time / 3)) == (offsetTrigger * -1))
		{
			stepAvailable = true;
		}
	}
}
