using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor 
{	
	public Controller characterController;
	private Rigidbody rigidBody;
	public string characterName;
	public Attributes characterAttributes;
	public Vector3 velocity;
	

	void Awake () 
	{
		if(characterController != null) 
			characterController.active = true;
		if(this.GetComponentInChildren<Rigidbody>() == true) 
			rigidBody = this.GetComponentInChildren<Rigidbody>();
	}
	
	void Update () 
	{
		if(rigidBody != null)
			velocity = rigidBody.velocity;
		if(IsFalling())
		 	rigidBody.AddForce(-Vector3.up * (rigidBody.mass * 10) * Time.deltaTime, ForceMode.Acceleration);
		Debug.Log("Grounded: " + IsGrounded());
	}

	void OnCollisionEnter(Collision other)
	{
		//Debug.Log(rigidBody.velocity.magnitude);
		if(rigidBody.velocity.magnitude > 0.2f)
			EventManager.TriggerEvent("PlayerCollision");
	}

	public virtual void Move(Vector3 direction)
	{
		if(rigidBody != null)
		{
			Vector3 newPosition = (((transform.forward / 100) * direction.z) + ((transform.right / 100) * direction.x)) * characterAttributes.speed;
			//Debug.Log("X: " + newPosition.x.ToString("F") + " Z: " + newPosition.z.ToString("F"));
			rigidBody.MovePosition(transform.position + newPosition);
		}
	}
	public virtual void Jump()
	{
		if(rigidBody != null)
		{
			rigidBody.AddForce(transform.up * characterAttributes.jumpForce, ForceMode.VelocityChange);
		}
	}

	public bool IsFalling()
	{
		if(velocity.y < 0)
			return true;
		return false;
	}

	public bool IsGrounded()
	{
		int layer = 1 << this.gameObject.layer;
		layer = ~layer;
		CapsuleCollider thisCollider = GetComponent<CapsuleCollider>();
		Vector3 spherePos = new Vector3(transform.position.x, transform.position.y - thisCollider.radius, transform.position.z);
		if(Physics.CheckSphere(spherePos, thisCollider.radius, layer))
			return true;
		else
			return false;
	}
}
