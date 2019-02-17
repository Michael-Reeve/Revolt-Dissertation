using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaRadius : MonoBehaviour 
{
	private ParticleSystem particleMain;
	private ParticleSystem.ShapeModule particleShape;
	private ParticleSystem.Particle[] particles;
	private ParticleCollisionEvent[] collisionEvents;
	public Electric conductor;
	public float radius;
	private float arcSpeed = 2f;

	void Awake()
	{
		particleMain = GetComponent<ParticleSystem>();
		particleShape = particleMain.shape;
		particles = new ParticleSystem.Particle[particleMain.main.maxParticles];
	}

	void Update()
	{
		//Attract();
		particleShape.radius = radius;
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
}
