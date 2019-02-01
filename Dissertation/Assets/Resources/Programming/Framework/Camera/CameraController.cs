using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CameraController : MonoBehaviour
{

	private UnityAction impact;
	private Vector3 initialRot;
	protected Vector3 velocity = Vector3.zero;
	public float sensitivity;
	public Vector2 clampXY;
	public bool active = true;
	public bool smoothed;
	public float smoothingAmount;
	protected float smoothing;
	protected Camera thisCamera;
	public GameObject possessed;
	public Vector3 offset;
	


	void OnEnable()
	{
		impact = new UnityAction (Impact);
		EventManager.StartListening("PlayerCollision", impact);
	}

	public virtual void Impact()
	{
		//active = !active;
	}

	public virtual bool possessedVisible()
	{
		return true;
	}

	public virtual void Awake()
	{
		thisCamera = GetComponent<Camera>();
		smoothing = smoothingAmount;
		initialRot = transform.eulerAngles;
		if(possessed != null)
		{
			if(possessed.transform.parent.GetComponent<PlayerController>())
			{
				sensitivity = possessed.transform.parent.GetComponent<PlayerController>().mouseSensitivity;
				possessed.transform.parent.GetComponent<PlayerController>().activeCamera = thisCamera;
			}
		}
	}

	protected bool IsOnScreen(Vector3 objectLocation)
	{
		Vector3 screenPoint = thisCamera.WorldToViewportPoint(objectLocation);
		bool onScreen = screenPoint.x > 0.2 && screenPoint.y > 0.2 && screenPoint.x < 0.8 && screenPoint.y < 0.8;
		return onScreen;
	}

	public virtual void MoveToPossessed(bool smoothed, float smoothing = 0)
	{
		if(smoothed == false)
			transform.position = possessed.transform.position + offset;
		else
		{
			transform.position = Vector3.Lerp(transform.position, possessed.transform.position, Time.deltaTime * smoothing);
		}
	}

	public virtual void RotatePossessed(Vector2 input, float sensitivity = 1f)
	{
		
	}

	protected Vector3 ClampPossessed(float xClamp = 0, float yClamp = 0)
	{
		Vector3 clampedRot = transform.eulerAngles;
		
		if (xClamp != 0)
			clampedRot.x = Mathf.Clamp(clampedRot.x, xClamp * -1, xClamp);
		if (yClamp != 0)
			clampedRot.y = Mathf.Clamp(clampedRot.y, yClamp * -1, yClamp);

		return clampedRot;
	}

 
}
