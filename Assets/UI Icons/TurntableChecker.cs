﻿using ForestOfChaosLib;
using UnityEngine;

public class TurntableChecker : FoCsBehavior
{
	public GameObject Prefab;
	public void OnEnable()
	{
		if(Turntable.InstanceNull)
			Instantiate(Prefab);
	}
}
