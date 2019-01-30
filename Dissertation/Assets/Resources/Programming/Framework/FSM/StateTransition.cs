using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StateTransition
{

	public State initState;
	[Space, Space]
	public State exitState;

	public bool Evaluate()
	{
		return true;
	}

}
