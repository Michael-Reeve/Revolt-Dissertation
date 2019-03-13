using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Electric : MonoBehaviour
{
	public bool active;
	public delegate void ChargeAction();
	public ChargeAction chargeAction;
	public List<Electric> conductingFrom;
	public List<Electric> conductingTo;
	public LayerMask layerMask;
	public float maxRadius = 5;
	public Vector3 electricOffset;
	protected int voltage = 0;
	public UnityEvent onVoltageChange;

	public int Voltage
	{
		get
		{
			return voltage;
		}
		set
		{
			if(value != voltage)
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
			else
			{
				onVoltageChange.Invoke();
			}
		}
	}

	public float GetRadius()
	{
		return maxRadius / 100 * voltage;
	}

	protected List<Electric> FindConductors()
	{
		List<Electric> foundElectrics = new List<Electric>();
		foundElectrics = Utility.GetInRadius<Electric>(maxRadius / 100 * voltage, transform.position);
		if(foundElectrics.Contains(this))
			foundElectrics.Remove(this);
		foundElectrics = SortConductors(foundElectrics);
		foundElectrics.TrimExcess();
		return foundElectrics;
	}

	private List<Electric> SortConductors(List<Electric> foundElectrics)
	{
		List<Electric> foundElectricsCopy = new List<Electric>();
		foundElectricsCopy.AddRange(foundElectrics);
		foreach(Electric electric in foundElectricsCopy)
		{
			if(conductingFrom.Contains(electric))
			{
				foundElectrics.Remove(electric);
			}
			Vector3 direction = (electric.transform.position + electric.electricOffset) - (transform.position + electricOffset);
			if(CanConnect(electric, transform.position + electricOffset, direction) == false)
				foundElectrics.Remove(electric);
		}
		return foundElectrics;
	}

	protected bool CanConnect(Electric electric, Vector3 origin, Vector3 direction)
	{
		RaycastHit raycastHit;
		if(Physics.Raycast(origin, Vector3.Normalize(direction), out raycastHit, voltage, layerMask))
		{
			Debug.DrawLine(transform.position, raycastHit.point, Color.red, 3f);
			if(raycastHit.collider.GetComponentInParent<Electric>() == electric)
			{
				return true;
			}
			else
			{
				Debug.Log(this.name + " Occluded By: " + raycastHit.collider.name);
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	/// Gets all electrics within the current radius
	/// If the electric exists in ConductingTo but is not found in the new check, it's added to the difference list.
	/// If an electric in difference is conducting from this, removes that reference on the object.
	protected void Difference()
	{
		List<Electric> electrics = FindConductors();
		List<Electric> difference = conductingTo.Except(electrics).ToList();
		foreach(Electric electric in difference)
		{
			if(electric.conductingFrom.Contains(this))
			{
				Debug.Log("Purging " + this.name + " references from " + electric.name);
				electric.conductingFrom.Remove(this);
				electric.conductingFrom.TrimExcess();
				electric.CalcultageVoltage();
			}
		}
	}

	protected void SetConductors()
	{
		foreach(Electric electric in conductingTo)
		{
			if(electric.conductingFrom.Contains(this))
				continue;
			electric.conductingFrom.Add(this);
		}
	}

	protected void ChargeConductors()
	{
		int distance = 5;
		foreach(Electric electric in conductingTo)
		{
			electric.CalcultageVoltage();
			if(electric.chargeAction != null)
				electric.chargeAction();
		}
	}

	protected void CalcultageVoltage()
	{
		float newVoltage = 0;
		int distance = 5;
		foreach(Electric electric in conductingFrom)
		{
			newVoltage += ((electric.Voltage / conductingFrom.Count) - distance);
		}
		this.Voltage = Mathf.RoundToInt(newVoltage);
	}

	public void UpdateConnections()
	{
		Difference();
		conductingTo = FindConductors();
		SetConductors();
		ChargeConductors();
	}

	public List<Electric> GetConnections(Electric origin)
	{
		List<Electric> allConnections = new List<Electric>();
		foreach(Electric electric in conductingTo)
		{
			allConnections.Add(electric);
			allConnections.AddRange(electric.GetConnections(this));
		}
		return allConnections;
	}

		public List<Electric> GetConnectionsFrom(Electric origin)
	{
		List<Electric> allConnections = new List<Electric>();
		foreach(Electric electric in conductingFrom)
		{
			allConnections.Add(electric);
			allConnections.AddRange(electric.GetConnectionsFrom(this));
		}
		return allConnections;
	}

}
