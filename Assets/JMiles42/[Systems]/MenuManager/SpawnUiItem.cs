﻿using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;

public class SpawnUiItem: JMiles2DBehavior {
	public GameObject Prefab;
	public int Amount = 1;
	public int TransformIndex;

	public void Start() {
		for (int i = Amount - 1; i >= 0; i--) {
			var go = Instantiate(Prefab);
			go.transform.parent = Transform;
			go.ResetLocalPosRotScale();
			go.transform.SetSiblingIndex(TransformIndex);
		}
	}
}