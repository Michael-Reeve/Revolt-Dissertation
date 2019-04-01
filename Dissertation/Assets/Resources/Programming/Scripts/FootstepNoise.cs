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

	public void FootstepSound()
	{
		audioSource.clip = footsteps[Random.Range(0, footsteps.Count)];
		audioSource.Play();
	}

}
