using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerKeyCode : MonoBehaviour 
{
	public Text keyText;
	public int key;

	void Start()
	{
		keyText.text = key.ToString();
	}

	public void Add(int value)
	{
		key += value;
		if(key >= 10)
		{
			key = 0;
		}
		else if (key <= -1)
		{
			key = 9;
		}
		UpdateText();
	}

	private void UpdateText()
	{
		keyText.text = key.ToString();
	}

}
