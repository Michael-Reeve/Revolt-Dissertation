using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject 
{
	[TextArea]
	public string subtitle;
	public AudioClip audio;
	public enum priority {Contextual, Dialogue};
	public priority dialoguePriority;
	public float length;
	
}
