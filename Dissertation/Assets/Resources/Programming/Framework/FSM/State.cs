using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
	public bool active;
	public StateMachine owner;
	public List <State> stateTransitions;

	public void RegisterState(StateMachine registerTo)
    {
		if(owner == null)
        	owner = registerTo;
		else
			Debug.Log("State already has an owner!");
    }

	public virtual void OnEnter()
	{

	}

	public virtual void OnLeave()
	{

	}

	public virtual void UpdateState()
	{
		if(active)
			StateUpdate();
	}

	protected virtual void StateUpdate()
	{
		
	}

	
	
}


