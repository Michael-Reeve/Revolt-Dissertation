using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectData
{
	public bool active;
	public Vector3 position;
	public Quaternion rotation;
	public Transform parent;

	public ObjectData(bool _active, Vector3 _position, Quaternion _rotation, Transform _parent)
	{
		active = _active;
		position = _position;
		rotation = _rotation;
		parent = _parent;
	}
}