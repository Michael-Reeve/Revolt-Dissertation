using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour 
{
	public bool active;
	public List<State> states = new List<State>();
	[Space, Space]
	public State currentState;
	public State nextState;
	public Controller controller;

	void Awake()
	{
		Register();
	}

	void Start()
	{
	}

	public void switchState(System.Type goTo)
	{
		State nextState = FindState(goTo);
		if(CheckTransition(nextState))
		{
			if(nextState.active == false)
			{
				currentState.active = false;
				currentState.OnLeave();
				nextState.active = true;
				nextState.OnEnter();
				currentState = nextState;   
			}
		}
	}

	private State FindState(System.Type stateType)
	{
		for(int i = 0; i < states.Count; i++)
		{
			if(states[i].GetType() == stateType)
			{
				return states[i];
			}
		}
		Debug.Log("Nothing");
		return null;
	}

	private bool CheckTransition(State state)
	{
		foreach(State transition in currentState.stateTransitions)
		{
			if(state == transition)
				return true;
		}
		return false;
	}


	void Update()
	{
		if(active)
			currentState.UpdateState();
	}

	void Register ()
    {
        for (int i = 0; i < states.Count; i++)
        {
            states[i].RegisterState(this);
        }
    }
}
