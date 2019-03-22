using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingImage : MonoBehaviour 
{
	public float scrollSpeed = 0.5f;
	public Image image;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float offset = Time.time * scrollSpeed;
		
	}
}
