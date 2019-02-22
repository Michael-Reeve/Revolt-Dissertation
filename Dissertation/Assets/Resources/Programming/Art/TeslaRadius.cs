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
	
	void Start()
	{
		UpdateRadius();
	}

	public void UpdateRadius()
	{
		radius = conductor.GetRadius();
		particleShape.radius = radius;
		if(radius == 0)
			this.gameObject.SetActive(false);
		else
			this.gameObject.SetActive(true);
	}
}
