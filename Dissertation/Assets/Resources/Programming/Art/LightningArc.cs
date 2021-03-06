﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArc : MonoBehaviour
{
	private ParticleSystem particleMain;
	private ParticleSystem.Particle[] particles;
	private ParticleCollisionEvent[] collisionEvents;
	public Electric conductor;
	public Electric origin;
	private float arcSpeed = 10f;
	
	void Awake()
	{
		particleMain = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[particleMain.main.maxParticles];
	}

	void Update()
	{
		Attract();
	}

	public void Attract()
	{
		particleMain.GetParticles(particles);
		for(int i = 0; i < particles.Length; i++)
		{
			if(conductor != null)
			{
				Vector3 particleDirection = (conductor.transform.position - particles[i].position).normalized;
				particles[i].velocity = particleDirection * arcSpeed;
				Debug.DrawRay(particles[i].position, particleDirection, Color.red, 5f);
			}
		}
		particleMain.SetParticles(particles, particles.Length);
	}

	void OnParticleCollision(GameObject other)
	{
		
	}
}

