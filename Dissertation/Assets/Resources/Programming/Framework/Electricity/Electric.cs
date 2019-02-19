using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Electric : MonoBehaviour
{
	public bool active;
	public delegate void ChargeAction(Electric obj = null);
	public ChargeAction chargeAction;
	public List<Electric> conductingTo = new List<Electric>();
	protected List<Electric> conductingFrom = new List<Electric>();
	protected List<GameObject> arcEffects = new List<GameObject>();
	public GameObject electricArc;
	public GameObject electricRadius;
	public LayerMask layerMask;
	public float arcRadius = 5;
	[SerializeField]
	protected int voltage = 0;

	void Start()
	{
		if(electricRadius)
			electricRadius.SetActive(false);
	}
	public int Voltage
	{
		get
		{
			return voltage;
		}
		set
		{
			if(value >= 100)
			{
				voltage = 100;
			}
			else if (value <= 0)
			{
				voltage = 0;
			}
			else
			{
				voltage = value;
			}
		}
	}

	protected void ResetEffects()
	{
		List<GameObject> effectsToRemove = arcEffects;
		foreach(GameObject arcObject in effectsToRemove)
		{
			Destroy(arcObject);
		}
		arcEffects.Clear();
	}
	
	public List<Electric> GetConnections(Electric origin)
	{
		List<Electric> allConnections = new List<Electric>();
		foreach(Electric electric in conductingTo)
		{
			allConnections.Add(electric);
			allConnections.AddRange(electric.GetConnections(this));
		}
		foreach(Electric electric in conductingFrom)
		{
			allConnections.Add(electric);
		}
		return allConnections;
	}

	public List<Electric> GetConnectionsTo(Electric origin)
	{
		List<Electric> allConnections = new List<Electric>();
		foreach(Electric electric in conductingTo)
		{
			allConnections.Add(electric);
			allConnections.AddRange(electric.GetConnectionsTo(this));
		}
		return allConnections;
	}

	protected void ClearConnections()
	{
		List<Electric> allConnections = GetConnections(this);
		foreach(Electric electric in allConnections)
		{
			Debug.Log("Cleared " + electric.gameObject.name);
			electric.ResetEffects();
			electric.conductingTo = new List<Electric>();
			electric.conductingFrom = new List<Electric>();
		}
	}

	public void GetConductors()
	{
		List<Electric> foundElectrics = FindConductors();
		SetParticleRadius();
		foreach(Electric electric in foundElectrics)
		{
			if(ConductorOccluded(electric) == false)
			{
				conductingTo.Add(electric);
				electric.conductingFrom.Add(this);
			}
		}
	}

	void SetParticleRadius()
	{
		if(arcRadius / 100 * voltage > 0)
		{
			if(electricRadius)
			{
				electricRadius.SetActive(true);
				electricRadius.GetComponentInChildren<TeslaRadius>().radius = arcRadius / 100 * voltage;
			}
		}
		else
		{
			if(electricRadius)
				electricRadius.SetActive(false);
		}
	}

	private bool ConductorOccluded(Electric electric)
	{
		RaycastHit raycastHit;
		if(Physics.Raycast(transform.position, Vector3.Normalize(electric.transform.position - transform.position), out raycastHit, voltage, layerMask))
		{
			Debug.Log(this.name + " Occluded By: " + raycastHit.collider.name);
			Debug.DrawLine(transform.position, raycastHit.point, Color.red, 3f);
			if(raycastHit.collider.GetComponent<Electric>() == electric)
			{
				return false;
			}
		}
		return true;
	}

	private List<Electric> FindConductors()
	{
		List<Electric> foundElectrics = new List<Electric>();
		foundElectrics = Utility.GetInRadius<Electric>(arcRadius / 100 * voltage, transform.position);
		if(foundElectrics.Contains(this))
			foundElectrics.Remove(this);
		List<Electric> foundElectricsCopy = new List<Electric>();
		foundElectricsCopy.AddRange(foundElectrics);
		foreach(Electric electric in foundElectricsCopy)
		{
			if(conductingFrom.Contains(electric) || conductingTo.Contains(electric))
			{
				foundElectrics.Remove(electric);
			}
		}
		return foundElectrics;
	}

	public virtual void ChargeConductors()
	{
		foreach(Electric electric in conductingTo)
		{
			electric.Voltage = electric.CalculateVoltage();
			if(electric.chargeAction != null)
				electric.chargeAction();
		}
	}

	public int CalculateVoltage()
	{
		int totalVoltage = 0;
		foreach(Electric electric in conductingFrom)
		{
			int distance = 5;
			if(electric.conductingTo.Count != 0)
				totalVoltage += (int)((electric.Voltage - distance) / electric.conductingTo.Count);
			else
				totalVoltage += (int)(electric.Voltage - distance);
			Debug.Log("current Voltage:" + totalVoltage + " | current Electric:" + electric.name);
		}
		return totalVoltage;
	}

	protected void CreateArc(List<Electric> linkedConductors)
	{
		foreach(Electric conductor in linkedConductors)
		{
			if(CheckEffectExists(conductor) == false)
			{
				Debug.Log("Test");
				GameObject arc = Instantiate(electricArc, transform.position, electricArc.transform.rotation);
				LightningArc arcElectric = arc.GetComponentInChildren<LightningArc>();
				arcElectric.origin = this; arcElectric.conductor = conductor;
				arcEffects.Add(arc); conductor.arcEffects.Add(arc);
			}
		}
	}

	protected bool CheckEffectExists(Electric conductor)
	{
		foreach(GameObject arc in conductor.arcEffects)
		{
			if(arc == null)
				break;
			LightningArc arcElectric = arc.GetComponentInChildren<LightningArc>();
			if(arcElectric.conductor == this || arcElectric.origin == this)
			{
				return true;
			}
		}
		return false;
	}

}
