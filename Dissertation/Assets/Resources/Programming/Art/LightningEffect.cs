using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour 
{
	public GameObject effect;
	public Electric electric;
	public List<Electric> conductors;
	private List<ParticleMagnet> effects = new List<ParticleMagnet>();

	void OnDisable()
	{
		RemoveEffects();
	}

	private void CreateEffect()
	{
		conductors = electric.conductingTo;
		foreach(Electric conductor in conductors)
		{
			GameObject newEffect = Instantiate(effect, transform.position + electric.electricOffset, Quaternion.identity);
			ParticleMagnet newMagnet = newEffect.GetComponentInChildren<ParticleMagnet>();
			effects.Add(newMagnet);
			newMagnet.conductor = conductor;
		}
	}

	public void RemoveEffects()
	{
		if(effects != null && effects.Count >= 1)
		{
			for(int i = 0; i < effects.Count; i++)
			{
				if(effects[i].gameObject != null)
					Destroy(effects[i].gameObject);
			}
			effects.Clear();
			effects.TrimExcess();
			effects = new List<ParticleMagnet>();
		}
	}

	public void UpdateEffects()
	{
		RemoveEffects();
		CreateEffect();
	}

}
