using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ElectricNew : MonoBehaviour
{
	public bool active;
	public delegate void ChargeAction();
	public ChargeAction chargeAction;
	public List<ElectricNew> conductingFrom;
	public List<ElectricNew> conductingTo;
	public LayerMask layerMask;
	public float maxRadius = 5;
	[SerializeField]
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

	protected List<ElectricNew> FindConductors()
	{
		List<ElectricNew> foundElectrics = new List<ElectricNew>();
		foundElectrics = Utility.GetInRadius<ElectricNew>(maxRadius / 100 * voltage, transform.position);
		if(foundElectrics.Contains(this))
			foundElectrics.Remove(this);
		List<ElectricNew> foundElectricsCopy = foundElectrics;
		foreach(ElectricNew electric in foundElectricsCopy)
		{
			if(conductingFrom.Contains(electric))
			{
				foundElectrics.Remove(electric);
			}
		}
		foundElectrics.TrimExcess();
		return foundElectrics;
	}

	protected bool CanConnect(ElectricNew electric, Vector3 origin, Vector3 direction)
	{
		RaycastHit raycastHit;
		if(Physics.Raycast(origin, Vector3.Normalize(direction), out raycastHit, voltage, layerMask))
		{
			Debug.DrawLine(transform.position, raycastHit.point, Color.red, 3f);
			if(raycastHit.collider.GetComponent<Electric>() == electric)
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

	protected void ChargeConductors()
	{
		int distance = 5;
		foreach(ElectricNew electric in conductingTo)
		{
			electric.conductingFrom.Add(this);
			electric.Voltage += ((this.Voltage / conductingTo.Count) - distance);
			if(electric.chargeAction != null)
				electric.chargeAction();
		}
	}

	public List<ElectricNew> GetConnections(ElectricNew origin)
	{
		List<ElectricNew> allConnections = new List<ElectricNew>();
		foreach(ElectricNew electric in conductingTo)
		{
			allConnections.Add(electric);
			allConnections.AddRange(electric.GetConnections(this));
		}
		return allConnections;
	}

}
