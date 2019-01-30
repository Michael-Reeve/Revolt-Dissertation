using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Statistic 
{
	public string name;
	public int max;
	[SerializeField] private int stat;
	public int statistic
	{
		get 
		{
			if (stat > max) 
				return max;
			else if (stat < 0) 
				return 0;
			else return stat;
		}
		set
		{
			if (stat > max) 
				stat = max;
			else if (stat < 0) 
				stat = 0;
			else stat = value;
		}
	}

	/// <summary>
	/// Constructs the statistic, the stat is set to 0 at the beginning prior to being set to the requested number through its property.
	/// </summary>
	public Statistic (int baseStat, int stat_max, int stat_min)
	{
		name = "";
		stat = 0;
		max = stat_max;
		statistic = baseStat;
		
	}
}
