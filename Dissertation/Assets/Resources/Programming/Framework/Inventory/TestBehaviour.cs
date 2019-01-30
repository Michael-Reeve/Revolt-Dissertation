using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestBehaviour", menuName = "Inventory/Behaviour/TestBehaviour", order = 0)]
public class TestBehaviour : ItemBehaviour
{
    public override void Use(Controller controller, Item item)
    {
        Debug.Log("Test Behaviour finally used!");
    }
}
