using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(fileName = "GUI_Database", menuName = "GUI Database")]
public class GUIDatabase : ScriptableObject
{
	public List<namedImage> cursors;
	[Space]
	public Sprite itemFrame;
	public string displayText;
}


[System.Serializable]
public struct namedImage
{
	public string name;
	public Sprite image;
}


