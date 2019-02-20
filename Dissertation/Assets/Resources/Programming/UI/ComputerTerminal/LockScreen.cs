using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreen : MonoBehaviour 
{
	public GameObject keyObject;
	public GameObject keyGrid;
	public List<ComputerKeyCode> keyCodes;

	public void GenerateKeys(string keycode)
	{
		for(int i = 0; i < keycode.Length; i++)
		{
			GameObject key = Instantiate(keyObject, keyGrid.transform);
			keyCodes.Add(key.GetComponent<ComputerKeyCode>());
		}
	}

	public string getKeyCode()
	{
		string keyCode = "";
		foreach(ComputerKeyCode keycode in keyCodes)
		{
			keyCode += keycode.key;
		}
		return keyCode;
	}
}
