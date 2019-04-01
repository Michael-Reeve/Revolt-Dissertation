using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour 
{
	public Animator animator;
	public FirstPersonCameraController cameraController;
	[Space]
	[Header("Settings")]
	public float speed = 1f;
	[Header("Feet")]
	public FootstepNoise leftFoot;
	public FootstepNoise rightFoot;

	private int velocity;
	private bool rightFootstep;

	void Update()
	{
		velocity = Mathf.RoundToInt(cameraController.possessed.movementVelocity.magnitude * 2);
		if(velocity == 0)
			animator.speed = 0.5f * speed;
		else if(cameraController.possessed.speedModifier == 1)
			animator.speed = 1f * speed;
		else if(cameraController.possessed.speedModifier == 2)
			animator.speed = 2f * speed;
	}

	public void FootstepEvent()
	{
		if(velocity != 0)
		{
			if(rightFootstep)
			{
				rightFoot.FootstepSound();
				rightFootstep = false;
			}
			else
			{
				leftFoot.FootstepSound();
				rightFootstep = true;
			}
		}
	}
}
