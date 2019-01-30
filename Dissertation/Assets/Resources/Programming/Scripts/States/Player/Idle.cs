using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Idle : State 
{
	private UnityAction KeyDownListener;

    public override void OnEnter()
    {
    }

    public override void OnLeave()
    {
    }

    protected override void StateUpdate()
    {
		InputRecieved();
    }

	public void InputRecieved()
	{
		if(Input.GetAxis("Horizontal") != 0)
		{
			owner.switchState(typeof(Walking));
		}
		if(Input.GetAxis("Vertical") != 0)
		{
			owner.switchState(typeof(Walking));
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(owner.controller.possessed.IsGrounded())
				owner.switchState(typeof(Jump));
		}
	}
}
