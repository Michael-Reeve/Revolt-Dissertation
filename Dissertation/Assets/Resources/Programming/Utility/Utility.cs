using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

	////// Generics //////
	/////////////////////
	//// Returns the interface specified, or null if it doesn't exist.
	public static interfaceType GetInterface<interfaceType>(object gameObject)
	{
		interfaceType gameObjectInterface = gameObject is interfaceType ? (interfaceType)gameObject : default(interfaceType);
		return gameObjectInterface;
	}

	public static List<interfaceType> GetInterface<interfaceType>(object[] gameObjects)
	{
		List<interfaceType> gameObjectInterfaces = new List<interfaceType>();
		for(int i = 0; i < gameObjects.Length; i++)
		{
			interfaceType gameObjectInterface = gameObjects[i] is interfaceType ? (interfaceType)gameObjects[i] : default(interfaceType);
			gameObjectInterfaces.Add(gameObjectInterface);
		}
		return gameObjectInterfaces;
	}

	public static T[] Swap<T>(T[] array, int itemIndex, int swapIndex) 
	{
		T tempItem = array[itemIndex];
		array[itemIndex] = array[swapIndex];
		array[swapIndex] = tempItem;
		return array;
	}

	public static List<T> GetInRadius<T>(float radius, Vector3 position)
	{
		Collider[] overlapped = Physics.OverlapSphere(position, radius);
		List<T> typeInstanceList = new List<T>();
		foreach(Collider collider in overlapped)
		{
			T colliderType = collider.gameObject.GetComponent<T>();
			if(collider.gameObject != null && colliderType != null)
			{
				typeInstanceList.Add(colliderType);
			}
		}
		return typeInstanceList;
	}


	
}


