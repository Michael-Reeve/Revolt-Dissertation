using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectricNew : MonoBehaviour
{
	public bool active;
	public delegate void ChargeAction(ElectricNew obj = null);
	public ChargeAction chargeAction;
	public List<ElectricNew> conductingFrom;
	public List<ElectricNew> conductingTo;
	public LayerMask layerMask;
	public float maxRadius = 5;
	[SerializeField]
	protected int voltage = 0;

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

	private List<ElectricNew> FindConductors()
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

	public void Conduct()
	{
		conductingTo = FindConductors();
		ChargeConductors();
	}

	private void ChargeConductors()
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

}
