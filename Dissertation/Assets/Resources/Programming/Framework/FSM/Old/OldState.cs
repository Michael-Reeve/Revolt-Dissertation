using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct OldState
{
	public string stateName;
	public int statePriority;
	public bool active;
	public UnityEvent stateProcess;
	public List <string> stateTransitions;

	/// <summary>
	/// Runs the stateProcess.
	/// </summary>
	public void StateUpdate()
	{
		if(active == true) {
			stateProcess.Invoke();
		}
	}
	
	
}


