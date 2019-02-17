using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor 
{	
	public Controller characterController;
	public Rigidbody rigidBody;
	public CapsuleCollider capsule;
	public float slopeAngle;
	public string characterName;
	public Attributes characterAttributes;
	public Vector3 velocity;
	public float speedModifier = 1;

	void Awake () 
	{
		if(characterController != null) 
			characterController.active = true;
		if(this.GetComponentInChildren<Rigidbody>() == true) 
			rigidBody = this.GetComponentInChildren<Rigidbody>();
		if(GetComponent<CapsuleCollider>())
			capsule = GetComponent<CapsuleCollider>();
	}
	
	void Update () 
	{
		RaycastHit raycastHit;
		if(Physics.Raycast(transform.position, (Vector3.up * -1), out raycastHit))
		{
			Vector3 SlopeForward = Vector3.Cross(transform.right, raycastHit.normal);
			float SlopeAngle = Vector3.SignedAngle(transform.forward, SlopeForward, Vector3.up);  
			slopeAngle = Vector3.SignedAngle(Vector3.up, raycastHit.normal, Vector3.up);
			transform.eulerAngles = new Vector3(-slopeAngle, transform.eulerAngles.y, 0);
		}
		if(rigidBody != null)
			velocity = rigidBody.velocity;
		if(IsFalling())
		 	rigidBody.AddForce(-Vector3.up * (rigidBody.mass * 10) * Time.deltaTime, ForceMode.Acceleration);
		//Debug.Log("Grounded: " + IsGrounded());
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
			direction = direction.normalized;
			Vector3 newPosition = (((transform.forward / 100) * direction.z) + ((transform.right / 100) * direction.x)) * characterAttributes.speed * speedModifier;
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
