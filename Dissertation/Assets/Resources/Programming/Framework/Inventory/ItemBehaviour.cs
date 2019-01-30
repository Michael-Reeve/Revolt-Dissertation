using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ItemBehaviour : ScriptableObject
{
    public abstract void Use(Controller controller, Item item);
}
