using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour, Interactible
{
	public bool lockedOn;
	public Vector2 cursorPosition;
	public RectTransform cursor;

	public void Interact(PlayerController controller)
	{
		RaycastHit raycastHit = controller.Raycast();
		Debug.Log(raycastHit.collider);
	}

	public void SetCursorPosition()
	{

	}

	public void LockOn()
	{

	}

}
