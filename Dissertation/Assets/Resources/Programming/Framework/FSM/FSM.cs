using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM 
{
	
	public static void switchState(State A, State B)
	{
		if(A.active == true && B.active == false)
		{
			A.active = false;
			A.OnLeave();
			B.active = true;
			B.OnEnter();
		}
		else if(A.active == false && B.active == true)
		{
			A.active = true;
			A.OnEnter();
			B.active = false;
			B.OnLeave();
		}
		else
		{
			Debug.Log("Invalid State Switch!");
		}
	}
}
