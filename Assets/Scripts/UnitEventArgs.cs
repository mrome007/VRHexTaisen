using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEventArgs : EventArgs
{
	public GameObject GameObject { get; private set; }

	public UnitEventArgs(GameObject gObj)
	{
		GameObject = gObj;
	}
}
