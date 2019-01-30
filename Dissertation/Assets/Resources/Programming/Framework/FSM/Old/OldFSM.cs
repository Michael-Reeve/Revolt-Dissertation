using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OldFSM : MonoBehaviour {
	public OldState[] states;
	[Space]
	public int currentState;
	public int nextState;

	[Space]
	public float updateDelay; 

	

	/// <summary>
	/// Adds state transitions and defines their requirements to transition.
	/// </summary>
	void Awake()
	{
		for(int i = 0; i < states.Length; i++) {
			if (states[i].active == true) {
				currentState = i;
			}
		}
		if(updateDelay > 0)
			UpdateState();
	}

	/// <summary>
	/// Iterates through existing states and updates them every time period as defined by the delay float.
	/// </summary>
	void UpdateState ()
	{
		states[currentState].StateUpdate ();
		Invoke("UpdateState", updateDelay);
	}

	/// <summary>
	/// Iterates through existing states and updates them if there is no delay.
	/// </summary>
	void Update ()
	{
		if(updateDelay == 0)
			states[currentState].StateUpdate ();
	}

	/// <summary>
	/// Transitions to the desired state so long as it is not active,
	/// sets the current state to inactive, and the desired state active.
	/// </summary>
	public void StateTransition (int nextState)
	{
		//Debug.Log (states [nextState].stateName);
		if (states [nextState].active == false && states[currentState].active == true) {
			if (states [currentState].stateTransitions.Contains (states [nextState].stateName)) {
				states [nextState].active = true;
				states [currentState].active = false;
				currentState = nextState;
			}
		}
	}

	/// <summary>
	///	Loops through the state array and gets the desired state by name.
	/// </summary>
	public int GetState(string stateName)
	{
		for(int i = 0; i < states.Length; i++)  {
			if(states[i].stateName == stateName){
				//Debug.Log(states[i].stateName + " returned.");
				return i;
			}
			
		}
		//Debug.Log("No state of name found.");
		return currentState;
	}

}
