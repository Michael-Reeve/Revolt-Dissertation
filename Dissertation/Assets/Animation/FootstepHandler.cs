using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepHandler : MonoBehaviour 
{
	public Headbob headbob;
	public void FootstepEvent()
	{
		headbob.FootstepEvent();
	}
}
