using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : State 
{
    public override void OnEnter()
    {
    }

    public override void OnLeave()
    {
    }

    protected override void StateUpdate()
    {
        owner.controller.possessed.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
		{
			owner.switchState(typeof(Idle));
		}
    }
}
