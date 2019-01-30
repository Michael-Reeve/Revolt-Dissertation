using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State
{

    public override void OnEnter()
    {
		  owner.controller.possessed.Jump();
    }

    public override void OnLeave()
    {
    }

    protected override void StateUpdate()
    {
		if(owner.controller.possessed.IsGrounded())
		{
      Debug.Log("SwaptoIdle");
			owner.switchState(typeof(Idle));
		}
    }
}
