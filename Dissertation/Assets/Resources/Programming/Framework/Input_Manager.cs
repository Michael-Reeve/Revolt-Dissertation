using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour 
{

	public static bool cursorLocked = false;
	public static bool shiftModifier = false;
	void Update () 
	{
		if(Input.anyKeyDown)
		{
			EventManager.TriggerEvent("InputKeyPress");
		}
		if(Input.anyKey)
		{
			EventManager.TriggerEvent("InputKey");
		}
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			shiftModifier = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			shiftModifier = false;
		}
	}

	public static void LockCursor(bool active)
	{
		if(active == true)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			cursorLocked = true;
		}
		else
		{
			cursorLocked = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
      		//Cursor.lockState = CursorLockMode.Confined;
		}
	}

}
