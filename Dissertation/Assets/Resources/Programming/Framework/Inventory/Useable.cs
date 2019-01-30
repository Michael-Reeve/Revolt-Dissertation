using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Useable Item", menuName = "Inventory/Useable", order = 0)]
public class Useable : Item
{
    [SerializeField]
    public ItemBehaviour behaviour;

    void OnEnable()
    {

    }

    public virtual void Use(Controller controller)
    {
        Debug.Log("TestUse! " + controller.name);
        behaviour.Use(controller, this);
    }
	
}

