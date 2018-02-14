﻿using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Grid;
using UnityEngine;

[AdvFolderName("Stocker")]
public class StandardPrefabPlacer: Placer
{
	public GridBlockListReference GridBlockList;
	public TransformRTRef PlaceableParent;
	public WorldObject Prefab;
	private WorldObject spawnedObject;

	public override void StartPlacing(GridPosition pos, Vector3 worldPos)
	{
		if(spawnedObject == null)
			spawnedObject = Instantiate(Prefab, worldPos, Quaternion.identity);
		spawnedObject.gameObject.SetActive(true);
		spawnedObject.transform.position = worldPos;
	}

	public override void UpdatePosition(GridPosition pos, Vector3 worldPos)
	{
		if(spawnedObject == null)
			return;
		spawnedObject.transform.position = worldPos;
	}

	public override void CancelPlacement()
	{
		spawnedObject?.gameObject.SetActive(false);
	}

	public override void ApplyPlacement(GridPosition pos, Vector3 worldPos)
	{
		spawnedObject.transform.position = pos.WorldPosition;
		spawnedObject.SetupObject(GridBlockList.GetBlock(pos));

		spawnedObject = null;
	}
}