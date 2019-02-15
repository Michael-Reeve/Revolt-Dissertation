using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Email", menuName = "Computer/Email", order = 0)]
public class Email : ScriptableObject 
{
	public string emailTitle;
	[TextArea]
	public string emailContents;

}
