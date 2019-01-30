using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour 
{

	public static bool cursorLocked = false;
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
	}

	public static void LockCursor(bool active)
	{
		if(active == true)
		{
			Cursor.lockState = CursorLockMode.Locked;
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
