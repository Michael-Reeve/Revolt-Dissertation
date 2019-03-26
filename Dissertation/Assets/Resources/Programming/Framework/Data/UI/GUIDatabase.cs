using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(fileName = "GUI_Database", menuName = "GUI Database")]
public class GUIDatabase : ScriptableObject
{
	public List<namedImage> cursors;
	[Space]
	public List<Hint> hints;
	[Space]
	public List<UISound> sounds;
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

[System.Serializable]
public struct Hint
{
	public string hintName;
	public string hintText;
}

[System.Serializable]
public struct UISound
{
	public string clipName;
	public AudioClip clip;
}


