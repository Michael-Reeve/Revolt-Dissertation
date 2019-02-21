using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour 
{
	public ParticleMagnet effect;
	public ElectricNew electric;
	public List<ElectricNew> conductors;
	private List<ParticleMagnet> effects;

	public void CreateEffect()
	{
		conductors = electric.conductingTo;
		foreach(ElectricNew conductor in conductors)
		{
			ParticleMagnet newEffect = Instantiate(effect, transform.position, Quaternion.identity);
			effects.Add(newEffect);
			newEffect.conductor = conductor.gameObject;
		}
	}

	public void RemoveEffects()
	{
		if(effects != null && effects.Count >= 1)
		{
			for(int i = 0; i < effects.Count; i++)
			{
				Destroy(effects[i].gameObject);
			}
			effects.Clear();
			effects.TrimExcess();
			effects = new List<ParticleMagnet>();
			Debug.Log(effects.Count);
		}
	}

}
